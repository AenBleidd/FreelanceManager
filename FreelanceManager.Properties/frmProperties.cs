using System;
using System.Windows.Forms;

namespace FreelanceManager.Properties
{
  public partial class frmProperties : Form
  {
    public frmProperties()
    {
      InitializeComponent();
    }

    public string getDBPath()
    {
      return edtDBPath.Text;
    }

    public void setDBPath(string strDBPath)
    {
      edtDBPath.Text = strDBPath;
    }

    public string getGoogleDriveRootPath()
    {
      return edtGoogleDriveRootPath.Text;
    }

    public void setGoogleDriveRootPath(string strGoogleDriveRootPath)
    {
      edtGoogleDriveRootPath.Text = strGoogleDriveRootPath;
    }

    public string getFreelanceDirectoryPath()
    {
      return edtFreelanceDirectoryPath.Text;
    }

    public void setFreelanceDirectoryPath(string strFreelanceDirectoryPath)
    {
      edtFreelanceDirectoryPath.Text = strFreelanceDirectoryPath;
    }

    public string get7ZipDirectoryPath()
    {
      return edt7ZipDirectoryPath.Text;
    }

    public void set7ZipDirectoryPath(string str7ZipDirectoryPath)
    {
      edt7ZipDirectoryPath.Text = str7ZipDirectoryPath;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (edtDBPath.TextLength == 0)
      {
        MessageBox.Show("Поле \"Расположение базы данных\" обязательно к заполнению", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (edtGoogleDriveRootPath.TextLength == 0)
      {
        MessageBox.Show("Поле \"Расположение папки Google Drive\" обязательно к заполнению", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (edtFreelanceDirectoryPath.TextLength == 0)
      {
        MessageBox.Show("Поле \"Расположение папки Freelance\" обязательно к заполнению", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (edt7ZipDirectoryPath.TextLength == 0)
      {
        MessageBox.Show("Поле \"Расположение папки 7-Zip\" обязательно к заполнению", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      DialogResult = DialogResult.OK;
    }

    private void btnDBPathOpen_Click(object sender, EventArgs e)
    {
      if (edtDBPath.TextLength != 0)
      {
        folderBrowserDialog.SelectedPath = edtDBPath.Text;
      }
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        edtDBPath.Text = folderBrowserDialog.SelectedPath;
      }
    }

    private void btnGoogleDriveRootPathOpen_Click(object sender, EventArgs e)
    {
      if (edtGoogleDriveRootPath.TextLength != 0)
      {
        folderBrowserDialog.SelectedPath = edtGoogleDriveRootPath.Text;
      }
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        edtGoogleDriveRootPath.Text = folderBrowserDialog.SelectedPath;
      }
    }

    private void btnFreelanceDirectoryPathOpen_Click(object sender, EventArgs e)
    {
      if (edtFreelanceDirectoryPath.TextLength != 0)
      {
        folderBrowserDialog.SelectedPath = edtFreelanceDirectoryPath.Text;
      }
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        edtFreelanceDirectoryPath.Text = folderBrowserDialog.SelectedPath;
      }
    }

    private void btn7ZipDirectoryPathOpen_Click(object sender, EventArgs e)
    {
      if (edt7ZipDirectoryPath.TextLength != 0)
      {
        folderBrowserDialog.SelectedPath = edt7ZipDirectoryPath.Text;
      }
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        edt7ZipDirectoryPath.Text = folderBrowserDialog.SelectedPath;
      }
    }
  }
}
