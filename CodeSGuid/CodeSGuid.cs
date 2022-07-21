using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using TSM = Tekla.Structures.Model;
using UI = Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;
using RCProjectObject.Controller;

namespace RCP_ExternalPropertyCSG
{
    public class PluginData
    {
        #region Fields
        //
        // Define the fields specified on the Form.
        //

        [StructuresField("AccordingSelected")]
        public int AccordingSelected;

        #endregion
    }
    [Plugin("RCP_ExternalProperty_CodeSGuid")]
    [PluginUserInterface("RCP_ExternalPropertyCSG.MainForm")]
    [InputObjectDependency(InputObjectDependency.NOT_DEPENDENT)]
    public class CodeSGuid : PluginBase
    {
        #region Fields
        private TSM.Model _Model;
        private PluginData _Data;

        private ArrayList _BaseAttribute = new ArrayList();

        #endregion

        #region Properties
        private TSM.Model Model
        {
            get { return this._Model; }
            set { this._Model = value; }
        }

        private PluginData Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }
        public List<TSM.Part> listParts { get; set; }
        #endregion

        #region Constructor
        public CodeSGuid(PluginData data)
        {
            Model = new TSM.Model();
            Data = data;
        }
        #endregion

        #region Overrides
        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> InputObjcts = new List<InputDefinition>();

            Point inPoint = PickAPoint("������� ����� ������");            
           
            InputObjcts.Add(new InputDefinition(inPoint));

            return InputObjcts;
        }

        public override bool Run(List<InputDefinition> Input)
        {
            try
            {
                GetValuesFromDialog();
                                
                if ((bool)_BaseAttribute[0])
                {
                    var modelObject = GetModelObjectEnumerator();

                    var iNTom = new Tom();
                    iNTom.DeserializeXML();

                    while (modelObject.MoveNext())
                    {
                        if (modelObject.Current is TSM.Part part)
                        {
                            var code = string.Empty;
                            part.GetUserProperty("RCP_CODE_PART", ref code);

                            if (code != string.Empty)
                            {
                                var album = iNTom.GetAlbumForChiper(code);

                                if (album != null)
                                {
                                    part.SetUserProperty("RCP_CODE_SGUID", album.InGUID.ShortGuid);
                                }
                            }
                        }
                    }
                } 
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.ToString());
            }

            return true;
        }
        #endregion

        #region Private methods
        private TSM.ModelObjectEnumerator GetModelObjectEnumerator()
        {
            var filterCollection = GetFilterCollection();

            var modelObjectEnum = Model.GetModelObjectSelector().GetObjectsByFilter(filterCollection);

            return modelObjectEnum;
        }

        private BinaryFilterExpressionCollection GetFilterCollection()
        {
            var filterCollection = new BinaryFilterExpressionCollection();
            filterCollection.Add(new BinaryFilterExpressionItem(GetTypePart(), BinaryFilterOperatorType.BOOLEAN_AND));//������, ��� ���������� ������ ������.
            filterCollection.Add(new BinaryFilterExpressionItem(GetMainPart()));//������, ��� ���������� ������ ������� ������.

            return filterCollection;
        }

        private BinaryFilterExpression GetTypePart()
        {
            var objectType1 = new ObjectFilterExpressions.Type();
            var objectNumeric1 = new NumericConstantFilterExpression(TeklaStructuresDatabaseTypeEnum.PART); //��� ������� - 2 ��. TeklaStructuresDatabaseTypeEnum Enumeration
            var expression1 = new BinaryFilterExpression(objectType1, NumericOperatorType.IS_EQUAL, objectNumeric1);
            return expression1;
        }
        private BinaryFilterExpression GetMainPart()
        {
            PartFilterExpressions.PrimaryPart partPrimary = new PartFilterExpressions.PrimaryPart();
            StringConstantFilterExpression partIsPrimary = new StringConstantFilterExpression("1");
            var expression1 = new BinaryFilterExpression(partPrimary, StringOperatorType.IS_EQUAL, partIsPrimary);
            return expression1;
        }

        /// <summary>
        /// Gets the values from the dialog and sets the default values if needed
        /// </summary>
        private void GetValuesFromDialog()
        {     

            if (IsDefaultValue(Data.AccordingSelected))
            {
                _BaseAttribute.Add(false);
            }
            else
            {
                 if (Data.AccordingSelected == 1)
                {
                    _BaseAttribute.Add(true);
                }
                else
                {
                    _BaseAttribute.Add(false);
                }
            }
            
        }

        private Point PickAPoint(string prompt = "Pick a point")
        {
            Point myPoint = null;
            try
            {
                var picker = new UI.Picker();
                myPoint = picker.PickPoint(prompt);
            }
            catch (Exception ex)
            {
                if (ex.Message != "User interrupt")
                {
                    Console.WriteLine(ex.Message + ex.StackTrace);
                }
            }

            return myPoint;
        }
        // Write your private methods here.

        #endregion
    }
}
