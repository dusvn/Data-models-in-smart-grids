using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Control;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkModelClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientGDA clientGDA = new ClientGDA();
        public static  bool finded = false; 
        public static bool info = false;
        public static bool find2 = false;
        public static bool find3 = false;
        public static bool info2 = false;
        public static bool info3 = false;
        public static bool cb = false; 
        public static List<ModelCode> selectedAttributes = new List<ModelCode>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           GetAllResources();
        }


        private void GetAllResources()
        {
            try
            {
                List<long> resources = clientGDA.GetAllGids();
                listResources.ItemsSource = resources;
                
            }
            catch (Exception ex)
            {
                string message = string.Format("GetAllResources failed. {0}", ex.Message);
                MessageBox.Show(message);
            }
        }


        public int check()
        {
            int c = 0;
            foreach(var item in selectBoxes.Items)
            {
                if(item is CheckBox cb && cb.IsChecked==true)
                {
                    c++;
                }
            }

            return c; 
        }

        public int check2()
        {

            int c = 0;
            foreach (var item in TypeAttributes.Items)
            {
                if (item is CheckBox cb && cb.IsChecked == true)
                {
                    c++;
                }
            }

            return c;
        }

        public int check3()
        {
            int c = 0;
            foreach (var item in propExValues.Items)
            {
                if (item is CheckBox cb && cb.IsChecked == true)
                {
                    c++;
                }
            }

            return c;

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
           int selectedCount = check();

           
            if (selectedCount == 0)
            {
                MessageBox.Show("By default we will find values for all attributes.");
                if (info == false)
                {
                    
                    InfoAboutSelectedGID();
                    info = true;
                    
                }
                else InfoAboutSelectedGID();

            }
            else
            {
                long selectedValueAsLong = Convert.ToInt64(listResources.SelectedValue); 
               
                
                List<ModelCode> mc = findCheckedModelCodes();

                string allValue = new ClientGDA().GetValues(selectedValueAsLong, mc);
                AttributesList.Items.Add(allValue);

            }
            

        }

        public List<ModelCode> findCheckedModelCodes()
        {
            List<ModelCode> l = new List<ModelCode>();
            foreach (var item in selectBoxes.Items)
            {
                if (item is CheckBox cb && cb.IsChecked == true)
                {

                    if (TryParseModelCode(cb.Content.ToString(), out ModelCode modelCodeEnumValue))
                    {
                        l.Add(modelCodeEnumValue);
                    }
                }
            }

            return l;

        }

        public List<ModelCode> findCheckedModelCodesRV()
        {
            List<ModelCode> l = new List<ModelCode>();
            foreach (var item in propExValues.Items)
            {
                if (item is CheckBox cb && cb.IsChecked == true)
                {

                    if (TryParseModelCode(cb.Content.ToString(), out ModelCode modelCodeEnumValue))
                    {
                        l.Add(modelCodeEnumValue);
                    }
                }
            }

            return l;

        }

        public List<ModelCode> findCheckedModelCodesExtendedValues()
        {
            List<ModelCode> l = new List<ModelCode>();
            foreach (var item in TypeAttributes.Items)
            {
                if (item is CheckBox cb && cb.IsChecked == true)
                {

                    if (TryParseModelCode(cb.Content.ToString(), out ModelCode modelCodeEnumValue))
                    {
                        l.Add(modelCodeEnumValue);
                    }
                }
            }

            return l;

        }

        public List<ModelCode> findCheckedModelCodesRelatedValues()
        {
            List<ModelCode> l = new List<ModelCode>();
            foreach (var item in propExValues.Items)
            {
                if (item is CheckBox cb && cb.IsChecked == true)
                {

                    if (TryParseModelCode(cb.Content.ToString(), out ModelCode modelCodeEnumValue))
                    {
                        l.Add(modelCodeEnumValue);
                    }
                }
            }

            return l;

        }

        private bool TryParseModelCode(string value, out ModelCode result)
        {
            return Enum.TryParse(value.Replace("__", "_"), out result);
        }

        private void InfoAboutSelectedGID()
        {
            dmsTypeLabel.Text = GetDMSTypeForGID();
            

            long selectedValueAsLong = Convert.ToInt64(listResources.SelectedValue);
            
            List<ModelCode> modelCodes = FindAttributesForGID(selectedValueAsLong);
            
            string allValue = new ClientGDA().GetValues(selectedValueAsLong,modelCodes);
            AttributesList.Items.Add(allValue);

            
        }

        public static List<ModelCode> FindAttributesModelCode(ModelCode modelKod)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIds(modelKod);

            return lista;
        }

        /// <summary>
        /// Return dmsType from 16bit gid 
        /// </summary>
        /// <returns></returns>
        private string GetDMSTypeForGID()
        {
            string selectedValueString = string.Format("0x{0:x16}", listResources.SelectedValue);
            char[] items = selectedValueString.Split('x')[1].ToCharArray();

            string dmsType = new string(items, 4, 4);

            if (String.Compare(dmsType, "0001") == 0)
            {
                return "FREQUENCYCONVERTER";

            }else if (String.Compare(dmsType, "0002") == 0)
            {
                return "SHUNTCOMPENSATOR";
            }
            else if(String.Compare(dmsType, "0003") == 0)
            {
                return "STATICVARCOMPENSATOR";
            }
            else if(String.Compare(dmsType,"0004")== 0)
            {
                return "REGULATINGCONTROL";
            }else if (String.Compare(dmsType, "0005") == 0)
            {
                return "CONTROL";
            }else if(String.Compare(dmsType,"0006") == 0)
            {
                return "TERMINAL";
            }
            else if(String.Compare(dmsType,"0007") == 0)
            {
                return "REACTIVECAPABILITYCURVE";
            }else if(String.Compare(dmsType,"0008") == 0)
            {
                return "SYNCHRONOUSMACHINE";
            }

            else return "CURVEDATA";

           
        }



        /// <summary>
        /// For gid returns all attributes 
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static List<ModelCode> FindAttributesForGID(long gid)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIdsForEntityId(gid);

            return lista;
        }

        

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (listResources.SelectedItem == null)
            {
                MessageBox.Show("Unable to find info, select GID.", "Find error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (finded = false)
                {
                    DisplayAttributes();
                    finded = true;
                }
                else
                {
                    AttributesList.Items.Clear();
                    dmsTypeLabel.Text = string.Empty;   
                    selectBoxes.Items.Clear();   
                    DisplayAttributes();
                }
                
            }
        }

        
        private void DisplayAttributes()
        {
            long selectedValueAsLong = Convert.ToInt64(listResources.SelectedValue);
            List<ModelCode> modelCodes = FindAttributesForGID(selectedValueAsLong);

            foreach (ModelCode modelCode in modelCodes)
            {
                CheckBox cb = new CheckBox();
                cb.Content = modelCode.ToString().Replace("_", "__");
                selectBoxes.Items.Add(cb);
            }
        }

        public static List<ModelCode> FindAttributesForDMS(DMSType dmstip)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIds(dmstip);

            return lista;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(DMSTypeCB.SelectedItem == null)
            {
                MessageBox.Show("Select one DMS type entity!", "Find error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (find2 == false)
                {
                    DisplayAttributesMethod2();
                    find2 = true;
                }
                else
                {
                    TypeAttributes.Items.Clear();   
                    DisplayAttributesMethod2(); 
                }

            }
        }

        private void DisplayAttributesMethod2()
        {
            DMSType mc = (DMSType)Enum.Parse(typeof(DMSType), ((ComboBoxItem)DMSTypeCB.SelectedItem).Content.ToString());
            List<ModelCode> attributesDMS = FindAttributesForDMS(mc);
            foreach (ModelCode modelCode in attributesDMS)
            {
                CheckBox cb = new CheckBox();
                cb.Content = modelCode.ToString().Replace("_", "__");
                TypeAttributes.Items.Add(cb);
            }
        }

        private void InfoAboutSelectedAttributesForDMSType(DMSType t,List<ModelCode> l)
        {
            string allValue = new ClientGDA().GetExtentValues(t, l);
            SelectedAttributesValues.Items.Add(allValue);
        }



        private void InfoAboutSelectedAttributesForRelatedValues(List<ModelCode> t, Association a,long gid)
        {
            string allValue = new ClientGDA().GetRelatedValues(gid, a, t);
            exValues.Items.Add(allValue);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DMSType selectedType = (DMSType)Enum.Parse(typeof(DMSType), ((ComboBoxItem)DMSTypeCB.SelectedItem).Content.ToString());
            List<ModelCode> modelCodes = FindAttributesForDMS(selectedType);
           

            int c = check2();
            if(c == 0)
            {
                MessageBox.Show("By default we will find values for all attributes.");
                if (info2 == false)
                {

                    InfoAboutSelectedAttributesForDMSType(selectedType, modelCodes);
                    info2 = true;

                }
                else InfoAboutSelectedAttributesForDMSType(selectedType,modelCodes);

            }
            else
            {   
                List<ModelCode> MC = findCheckedModelCodesExtendedValues();
                InfoAboutSelectedAttributesForDMSType(selectedType, MC);    
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            List<long> resources = clientGDA.GetAllGids();

            resources.ForEach(item => sourceGID.Items.Add(item));
        }

        public static List<ModelCode> FindAtrGetExValues(long gid3)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIdsForEntityId(gid3);
            List<ModelCode> rez = new List<ModelCode>();

            foreach (ModelCode mc in lista)
            {
                if (Property.GetPropertyType(mc) == PropertyType.Reference || Property.GetPropertyType(mc) == PropertyType.ReferenceVector)
                {
                    rez.Add(mc);
                }

            }

            return rez;

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if(sourceGID.SelectedItem== null)
            {
                MessageBox.Show("Unable to find info, select GID.", "Find error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!cb)
                {
                    List<ModelCode> reference = FindAtrGetExValues((long)sourceGID.SelectedItem);
                    List<ModelCode> tipovi = clientGDA.GetDMSTypes();


                    reference.ForEach(item => propId.Items.Add(new ComboBoxItem { Content = item }));
                    tipovi.ForEach(item => dmsTypes.Items.Add(new ComboBoxItem { Content = item }));
                    cb = true;
                }
                else
                {
                    propId.Items.Clear();
                    List<ModelCode> reference = FindAtrGetExValues((long)sourceGID.SelectedItem);
                    List<ModelCode> tipovi = clientGDA.GetDMSTypes();
                    reference.ForEach(item => propId.Items.Add(new ComboBoxItem { Content = item }));
                }

            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if(dmsTypes.SelectedItem== null) 
            {
                MessageBox.Show("Unable to find info, select dms type.", "Find error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!info3)
                {
                    DMSType dmt = (DMSType)Enum.Parse(typeof(DMSType), ((ComboBoxItem)dmsTypes.SelectedItem).Content.ToString());
                    List<ModelCode> d = FindAttributesForDMS(dmt);
                    d.ForEach(item => propExValues.Items.Add(new CheckBox { Content = item }));
                    info3 = true;
                }
                else
                {
                    propExValues.Items.Clear();
                    DMSType dmt = (DMSType)Enum.Parse(typeof(DMSType), ((ComboBoxItem)dmsTypes.SelectedItem).Content.ToString());
                    List<ModelCode> d = FindAttributesForDMS(dmt);
                    d.ForEach(item => propExValues.Items.Add(new CheckBox { Content = item }));
                }
            }

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            int n = check3();
            ModelCode t = (ModelCode)Enum.Parse(typeof(ModelCode), ((ComboBoxItem)dmsTypes.SelectedItem).Content.ToString());
            List<ModelCode> l = FindAttributesModelCode(t);

            Association a = new Association();
            a.Type = t;
            a.PropertyId = (ModelCode)Enum.Parse(typeof(ModelCode), ((ComboBoxItem)propId.SelectedItem).Content.ToString());


            if (n == 0)
            {
                MessageBox.Show("By default we will find values for all attributes.");
                if (!find3)
                {
                    string ss = clientGDA.GetRelatedValues((long)sourceGID.SelectedItem, a, l);
                    exValues.Items.Add(ss);
                }
                else
                {
                    string ss = clientGDA.GetRelatedValues((long)sourceGID.SelectedItem, a, l);
                    exValues.Items.Add(ss);
                }
               
            }
            else
            {
               

                List<ModelCode> mc = findCheckedModelCodesRV();
                string s = new ClientGDA().GetRelatedValues((long)sourceGID.SelectedItem, a, mc);
                exValues.Items.Add(s);

            }

        }


    }
}
