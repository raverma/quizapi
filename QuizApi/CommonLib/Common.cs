using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;
using Microsoft.Win32;
using Skyfactor.Smtp.DBLibrary;
using System.Configuration;
using System.Windows.Forms;

namespace Skyfactor.Smtp.Library
{
    public class WinServices
    {
      private string _serviceName;
      private string _hostName;
      public WinServices(string serviceName, string hostName)
      {
        _serviceName = serviceName;
        _hostName = hostName;
      }

      
      public bool StartService(int timeoutMilliseconds)
      {
        using (ServiceController service = new ServiceController(_serviceName, _hostName))
        { 
          try
          {
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            return true;
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex.InnerException);
          }
        }
      }

      public bool StopService(int timeoutMilliseconds)
      {
        using (ServiceController service = new ServiceController(_serviceName, _hostName))
        { 
          try
          {
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

            service.Stop();
            service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            return true;
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex.InnerException);
          }
        }
      }

      public string GetStatus()
      {
        using (ServiceController service = new ServiceController(_serviceName, _hostName))
        {
        try
          {
            switch (service.Status)
            {
              case ServiceControllerStatus.Running:
                return "Running";
              case ServiceControllerStatus.Stopped:
                return "Stopped";
              case ServiceControllerStatus.Paused:
                return "Paused";
              case ServiceControllerStatus.StopPending:
                return "Stopping";
              case ServiceControllerStatus.StartPending:
                return "Starting";
              default:
                return "Status Changing";
            }
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex.InnerException);
          }
        }
      }
 
    }

    public class RegistryEditor
    {
      private RegistryKey _key;
      private string _remoteName;

      public RegistryEditor(string keyName)
      {
        this._key = Registry.LocalMachine.OpenSubKey(keyName);
      }

      public RegistryEditor(string remoteName, string keyName)
      {
        this._key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remoteName).OpenSubKey(keyName);
        this._remoteName = remoteName;
      }

      public string RegistryKeyName
      {
        get
        {
          return this._key.Name;
        }
      }

      public List<string> GetChildren()
      {
        List<string> subKeyList = new List<string>();
        if (this._key.SubKeyCount > 0)
        {
          foreach (string subkey in this._key.GetSubKeyNames())
          {
            subKeyList.Add(subkey);
          }
        }
        return subKeyList;
      }

      public Object GetValue(string name)
      {
        return this._key.GetValue(name);
      }

      public void SaveValue(string key, string name, string value)
      {
        Microsoft.Win32.RegistryValueKind regValueKind = GetValueKind(name);
        RegistryKey regKey = Registry.LocalMachine.OpenSubKey(key);
        Registry.SetValue(regKey.ToString(), name, value, regValueKind);
      }

      public void SaveValueRemote(string key, string name, string value)
      {
        Microsoft.Win32.RegistryValueKind regValueKind = GetValueKind(name);
        RegistryKey regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, this._remoteName).OpenSubKey(key, true);
        regKey.SetValue(name, value, regValueKind);
      }

      private RegistryValueKind GetValueKind(string key)
      {
        if (key == string.Empty)
        {
          return RegistryValueKind.String;
        }
        else
        {
          return RegistryValueKind.DWord;
        }
      }

      public bool AddNewSubKey(string key, string subkeyName)
      {
        RegistryKey regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, this._remoteName).OpenSubKey(key, true);
        regKey.CreateSubKey(subkeyName);
        return true;
      }
    }

    public static class Authentication
    {
      public static bool EmployeeHasAccess(string userID)
      {
          DBAccess dba = new DBAccess();
          Parameter param = new Parameter("@UserId",ParamType.Varchar, userID);
          ICollection<Parameter> sqlParams = new System.Collections.ObjectModel.Collection<Parameter>();
          sqlParams.Add(param);
          var exists = dba.ExecuteProcedureScalar("EmployeeExist_sp", sqlParams, ConfigurationManager.ConnectionStrings["EBI.My.MySettings.EBIConnectionString"].ConnectionString);
          if (Convert.ToInt16(exists) == 1)
            return true;
          else
            return false;
      }
    }

    public class CreateForm<T> where T : Form, new()
    {
        private static T mInst;

        public CreateForm()
        {
        }

        public static void CreateInst(Form mdiParent)
        {
            if (mInst == null)
            {
                mInst = new T();
                mInst.Show();
            }
            else
            {
                if (mInst.WindowState == FormWindowState.Minimized || mInst.WindowState == FormWindowState.Maximized)
                    mInst.WindowState = FormWindowState.Normal;
                mInst.Focus();
            }
            mInst.MdiParent = mdiParent;
        }

        public static void CloseForm()
        {
            mInst = null;
        }
    }
}
