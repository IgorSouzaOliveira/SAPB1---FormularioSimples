using SAPbouiCOM;
using System;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;


namespace FormularioSimples
{
    public class FormularioSimples
    {
        public SAPbouiCOM.Application oApplication;
        public SAPbouiCOM.Form oForm;

        public void SetApplication()
        {
            SAPbouiCOM.SboGuiApi sboGuiApi = null;
            string sConnectionString = null;
            sboGuiApi = new SAPbouiCOM.SboGuiApi();
            sConnectionString = System.Convert.ToString(Environment.GetCommandLineArgs().GetValue(1));
            sboGuiApi.Connect(sConnectionString);
            oApplication = sboGuiApi.GetApplication(-1);
        }

        private void CreateMySimpleForm()
        {
            SAPbouiCOM.Item oItem = null;
            SAPbouiCOM.Button oButton = null;
            SAPbouiCOM.StaticText oStaticText = null;
            SAPbouiCOM.EditText oEditText = null;
            SAPbouiCOM.ComboBox oComboBox = null;

            // adicionar novo form - Padrão para tudo.
            SAPbouiCOM.FormCreationParams oCreationParams = null;
            oCreationParams = ((SAPbouiCOM.FormCreationParams)(oApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams)));
            oCreationParams.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Sizable;
            oCreationParams.UniqueID = "MeuFormSimples";

            oForm = oApplication.Forms.AddEx(oCreationParams);

            oForm.DataSources.UserDataSources.Add("EditSource",SAPbouiCOM.BoDataType.dt_SHORT_TEXT,20);
            oForm.DataSources.UserDataSources.Add("CombSource", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 20);

            //setar as propriedades do formulario

            oForm.Title = "Formulario Simples Souza";
            //posição inicial na tela
            oForm.Left = 400;
            oForm.Top = 100;
            //tamanho inicial na tela
            oForm.ClientHeight = 80;
            oForm.ClientWidth = 350;

            //adicionar botão OK
            oItem = oForm.Items.Add("1", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Left= 0;
            oItem.Width = 65;
            oItem.Top = 60;
            oItem.Height = 19;
            oButton = ((SAPbouiCOM.Button)(oItem.Specific));
            oButton.Caption = "OK";

            //adicionar botão Cancelar
            oItem = oForm.Items.Add("2", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Left = 70;
            oItem.Width = 65;
            oItem.Top = 60;
            oItem.Height = 19;
            oButton = ((SAPbouiCOM.Button)(oItem.Specific));
            oButton.Caption = "Cancel";

            //adicionar retangulo
            oItem = oForm.Items.Add("Rect1", SAPbouiCOM.BoFormItemTypes.it_RECTANGLE);
            oItem.Left = 0;
            oItem.Width = 344;
            oItem.Top = 5;
            oItem.Height = 49;

            //adicionar Label
            oItem = oForm.Items.Add("StaticTxt1", SAPbouiCOM.BoFormItemTypes.it_STATIC);
            oItem.Left = 7;
            oItem.Width = 148;
            oItem.Top = 8;
            oItem.Height = 14;
            //link  2 campos
            oItem.LinkTo = "EditText1";
            oStaticText = ((SAPbouiCOM.StaticText)(oItem.Specific));
            oStaticText.Caption = "Static Text 1";

            //adicionar outro Label
            oItem = oForm.Items.Add("StaticTxt2", SAPbouiCOM.BoFormItemTypes.it_STATIC);
            oItem.Left = 7;
            oItem.Width = 148;
            oItem.Top = 24;
            oItem.Height = 14;
            //link  2 campos
            oItem.LinkTo = "ComboBox1";
            oStaticText = ((SAPbouiCOM.StaticText)(oItem.Specific));
            oStaticText.Caption = "Static Text 2";

            //Adicionar o Edit text
            oItem = oForm.Items.Add("EditText1", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            oItem.Left = 157;
            oItem.Width = 163;
            oItem.Top = 8;
            oItem.Height = 14;
            oEditText = ((SAPbouiCOM.EditText)(oItem.Specific));
            oEditText.DataBind.SetBound(true,"","EditSource");
            oEditText.String = "Edit Text 1";

            //adicionar Combo Box
            oItem = oForm.Items.Add("ComboBox1", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
            oItem.Left = 157;
            oItem.Width = 163;
            oItem.Top = 24;
            oItem.Height = 14;
            oItem.DisplayDesc = false;
            oComboBox = ((SAPbouiCOM.ComboBox)(oItem.Specific));
            oComboBox.DataBind.SetBound(true, "", "CombSource");

            oComboBox.ValidValues.Add("0", "Selecione");
            oComboBox.ValidValues.Add("1", "Combo Value 1");
            oComboBox.ValidValues.Add("2", "Combo Value 2");
            oComboBox.ValidValues.Add("3", "Combo Value 3");

            oComboBox.Select("0", SAPbouiCOM.BoSearchKey.psk_ByValue);
            



        }

        private void SaveAsXML()
        {
            System.Xml.XmlDocument oXmlDoc = null;
            oXmlDoc = new System.Xml.XmlDocument();
            string sXmlString = null;
            sXmlString = oForm.GetAsXML();
            oXmlDoc.LoadXml(sXmlString);

            string sPath = null;
            sPath = System.IO.Directory.GetParent(Application.StartupPath).ToString();
            

            oXmlDoc.Save(sPath +@"\FormSimples.xml");
            oApplication.SetStatusBarMessage("Dir: " + sPath, SAPbouiCOM.BoMessageTime.bmt_Short, false);
        }
        public FormularioSimples() { 
        
            SetApplication();
            CreateMySimpleForm();

            oForm.Visible = true;

            SaveAsXML();


        }
    }
}
