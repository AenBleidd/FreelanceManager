using System.Windows.Forms;

// Windows registry
using Microsoft.Win32;


namespace FreelanceManager
{
  public class fmProperties
  {
    private const string regMainPath = "Software\\Freelance Manager";
    private const string regstrDBPath = "DBPath";
    private const string regstrFreelanceDirectoryPath = "FreelanceDirectoryPath";
    private const string regstrGooglDriveRootPath = "GoogleDriveRootPath";
    private const string regstr7ZipDirectoryPath = "7ZipDirectoryPath";

    public void Load(bool showWindow = true)
    {
      bool result = true;
      RegistryKey key = Registry.CurrentUser.OpenSubKey(regMainPath);
      if (key == null)
      {
        result = false;
      }
      else
      {
        object t;
        t = key.GetValue(regstrDBPath);
        if (t == null || t.ToString() == "")
        {
          result = false;
        }
        else
        {
          strDBPath = t.ToString();
        }
        t = key.GetValue(regstrFreelanceDirectoryPath);
        if (t == null || t.ToString() == "")
        {
          result = false;
        }
        else
        {
          strFreelanceDirectoryPath = t.ToString();
        }
        t = key.GetValue(regstrGooglDriveRootPath);
        if (t == null || t.ToString() == "")
        {
          result = false;
        }
        else
        {
          strGoogleDriveRootPath = t.ToString();
        }
        t = key.GetValue(regstr7ZipDirectoryPath);
        if (t == null || t.ToString() == "")
        {
          result = false;
        }
        else
        {
          str7ZipDirectoryPath = t.ToString();
        }
      }
      if (key != null) key.Close();
      if (result == false && showWindow)
      {
        frmProperties frm = new frmProperties();
        setOptionsToForm(frm);
        if (frm.ShowDialog() == DialogResult.OK)
        {
          getOptionsFromForm(frm);
          Save();
        }
      }
    }

    public void showPropertiesWindow()
    {
      Load(false);
      frmProperties frm = new frmProperties();
      setOptionsToForm(frm);
      if (frm.ShowDialog() == DialogResult.OK)
      {
        getOptionsFromForm(frm);
        Save();
      }
    }

    private void setOptionsToForm(frmProperties frm)
    {
      frm.setDBPath(strDBPath);
      frm.setFreelanceDirectoryPath(strFreelanceDirectoryPath);
      frm.setGoogleDriveRootPath(strGoogleDriveRootPath);
      frm.set7ZipDirectoryPath(str7ZipDirectoryPath);
    }

    private void getOptionsFromForm(frmProperties frm)
    {
      strDBPath = frm.getDBPath();
      strGoogleDriveRootPath = frm.getGoogleDriveRootPath();
      strFreelanceDirectoryPath = frm.getFreelanceDirectoryPath();
      str7ZipDirectoryPath = frm.get7ZipDirectoryPath();
    }

    public void Save()
    {
      RegistryKey key = Registry.CurrentUser.CreateSubKey(regMainPath);
      key.SetValue(regstrDBPath, strDBPath);
      key.SetValue(regstrFreelanceDirectoryPath, strFreelanceDirectoryPath);
      key.SetValue(regstrGooglDriveRootPath, strGoogleDriveRootPath);
      key.SetValue(regstr7ZipDirectoryPath, str7ZipDirectoryPath);
      key.Close();
    }
    public string strDBPath
    {
      get { return _strDBPath; }
      set { _strDBPath = value; }
    }
    public string strFreelanceDirectoryPath
    {
      get { return _strFreelanceDirectoryPath; }
      set { _strFreelanceDirectoryPath = value; }
    }
    public string strGoogleDriveRootPath
    {
      get { return _strGoogleDriveRootPath; }
      set { _strGoogleDriveRootPath = value; }
    }
    public string str7ZipDirectoryPath
    {
      get { return _str7ZipDirectoryPath; }
      set { _str7ZipDirectoryPath = value; }
    }
    private string _strDBPath;
    private string _strFreelanceDirectoryPath;
    private string _strGoogleDriveRootPath;
    private string _str7ZipDirectoryPath;
  }
}
