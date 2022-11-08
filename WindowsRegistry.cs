/******************************************************************************
**  Copyright(c) 2022 ignackoo. All rights reserved.
**
**  Licensed under the MIT license.
**  See LICENSE file in the project root for full license information.
**
**  This file is a part of the C# Registry Library.
** 
**  Windows Registry CRUD
**
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;


namespace Library
{
    public enum WindowsRegistryCategory
    {
        HKEY_CLASSES_ROOT,
        HKEY_CURRENT_USER,
        HKEY_LOCAL_MACHINE,
        HKEY_USERS,
        HKEY_CURRENT_CONFIG
    }

    public class WindowsRegistry
    {
        public WindowsRegistryCategory RegistryCategory { get; set; }

        public WindowsRegistry()
        { 
        }

        public WindowsRegistry(WindowsRegistryCategory winregcat)
        {
            this.RegistryCategory = winregcat;
        }


        /// <summary>
        /// Checks if the registry entry at the given path exists.
        /// </summary>
        /// <param name="path">Registry path.</param>
        /// <returns>True/False</returns>
        public bool Check(string path)
        {
            try
            {
                RegistryKey regkey;
                if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CLASSES_ROOT)
                {
                    regkey = Registry.ClassesRoot.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_USER)
                {
                    regkey = Registry.CurrentUser.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_LOCAL_MACHINE)
                {
                    regkey = Registry.LocalMachine.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_USERS)
                {
                    regkey = Registry.Users.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_CONFIG)
                {
                    regkey = Registry.CurrentConfig.OpenSubKey(path);
                }
                else
                    return (false);
                if (regkey == null) return (false);
                regkey.Close();
                return (true);
            }
            catch(Exception ex)
            {
                Exception e = ex;
                return (false);
            }
        }

        /// <summary>
        /// Creates a registry entry at the given path with given key and value.
        /// </summary>
        /// <param name="path">Registry subkey path to create.</param>
        /// <param name="key">Name of the key in the entry.</param>
        /// <param name="value">Key value.</param>
        /// <returns>True/False</returns>
        public bool Create(string path, string key, string value)
        {
            try
            {
                RegistryKey regkey;
                if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CLASSES_ROOT)
                {
                    Registry.ClassesRoot.CreateSubKey(path);
                    regkey = Registry.ClassesRoot.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_USER)
                {
                    Registry.CurrentUser.CreateSubKey(path);
                    regkey = Registry.CurrentUser.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_LOCAL_MACHINE)
                {
                    Registry.LocalMachine.CreateSubKey(path);
                    regkey = Registry.LocalMachine.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_USERS)
                {
                    Registry.Users.CreateSubKey(path);
                    regkey = Registry.Users.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_CONFIG)
                {
                    Registry.CurrentConfig.CreateSubKey(path);
                    regkey = Registry.CurrentConfig.OpenSubKey(path, true);
                }
                else
                    return (false);
                regkey.SetValue(key, value);
                regkey.Close();
                return (true);
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return (false);
            }
        }

        /// <summary>
        /// Reads the registry value at the given path with the given key name.
        /// </summary>
        /// <param name="path">Registry path.</param>
        /// <param name="key">Registry key.</param>
        /// <returns>Key value</returns>
        public string Read(string path, string key)
        {
            try
            {
                RegistryKey regkey;
                if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CLASSES_ROOT)
                {
                    regkey = Registry.ClassesRoot.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_USER)
                {
                    regkey = Registry.CurrentUser.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_LOCAL_MACHINE)
                {
                    regkey = Registry.LocalMachine.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_USERS)
                {
                    regkey = Registry.Users.OpenSubKey(path);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_CONFIG)
                {
                    regkey = Registry.CurrentConfig.OpenSubKey(path);
                }
                else
                    return (null);
                string regvalue = regkey.GetValue(key).ToString();
                regkey.Close();
                if (string.IsNullOrEmpty(regvalue)) return (null);
                return (regvalue);
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return (string.Empty);
            }

        }

        /// <summary>
        /// Edits the registry at the given path with the new value.
        /// </summary>
        /// <param name="path">Registry path.</param>
        /// <param name="key">Registry key.</param>
        /// <param name="value">New value.</param>
        /// <returns>True/False</returns>
        public bool Update(string path, string key, string value)
        {
            try
            {
                RegistryKey regkey;
                if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CLASSES_ROOT)
                {
                    regkey = Registry.ClassesRoot.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_USER)
                {
                    regkey = Registry.CurrentUser.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_LOCAL_MACHINE)
                {
                    regkey = Registry.LocalMachine.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_USERS)
                {
                    regkey = Registry.Users.OpenSubKey(path, true);
                }
                else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_CONFIG)
                {
                    regkey = Registry.CurrentConfig.OpenSubKey(path, true);
                }
                else
                    return (false);
                RegistrySecurity regsec = regkey.GetAccessControl(AccessControlSections.Access);
                regsec.SetGroup(new NTAccount("Administrators"));
                regsec.SetOwner(new NTAccount("Administrators"));
                regsec.SetAccessRuleProtection(false, false);
                regkey.SetAccessControl(regsec);
                regkey.SetValue(key, value);
                regkey.Close();
                return (true);
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return (false);
            }
        }

        /// <summary>
        /// Deletes the registry entry at the given path.
        /// </summary>
        /// <param name="path">Registry path.</param>
        /// <returns>True/False</returns>
        public bool Delete(string path, string key)
        {
            try
            {
                if (key == null)
                {
                    // delete whole subkey
                    if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CLASSES_ROOT)
                    {
                        Registry.ClassesRoot.DeleteSubKey(path);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_USER)
                    {
                        Registry.CurrentUser.DeleteSubKey(path);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_LOCAL_MACHINE)
                    {
                        Registry.LocalMachine.DeleteSubKey(path);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_USERS)
                    {
                        Registry.Users.DeleteSubKey(path);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_CONFIG)
                    {
                        Registry.CurrentConfig.DeleteSubKey(path);
                    }
                    else
                        return (false);
                }
                else
                {
                    // delete key value pair
                    RegistryKey regkey;
                    if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CLASSES_ROOT)
                    {
                        regkey = Registry.ClassesRoot.OpenSubKey(path, true);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_USER)
                    {
                        regkey = Registry.CurrentUser.OpenSubKey(path, true);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_LOCAL_MACHINE)
                    {
                        regkey = Registry.LocalMachine.OpenSubKey(path, true);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_USERS)
                    {
                        regkey = Registry.Users.OpenSubKey(path, true);
                    }
                    else if (this.RegistryCategory == WindowsRegistryCategory.HKEY_CURRENT_CONFIG)
                    {
                        regkey = Registry.CurrentConfig.OpenSubKey(path, true);
                    }
                    else
                        return (false);
                    regkey.DeleteValue(key);
                    regkey.Close();
                }
                return (true);
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return (false);
            }
        }
    }
}
