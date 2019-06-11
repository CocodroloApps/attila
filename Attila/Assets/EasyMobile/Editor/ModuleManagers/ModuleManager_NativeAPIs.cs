using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace EasyMobile.Editor
{
    internal class ModuleManager_NativeAPIs : CompositeModuleManager
    {
        #region Singleton

        private static ModuleManager_NativeAPIs sInstance;

        private ModuleManager_NativeAPIs()
        {
        }

        public static ModuleManager_NativeAPIs Instance
        {
            get
            {
                if (sInstance == null)
                    sInstance = new ModuleManager_NativeAPIs();
                return sInstance;
            }
        }

        #endregion

        #region implemented abstract members of ModuleManager

        public override Module SelfModule
        {
            get
            {
                return Module.NativeApis;
            }
        }

        #endregion

        #region implemented abstract members of CompositeModuleManager

        public override List<string> AndroidManifestTemplatePathsForSubmodule(Submodule submod)
        {
            #if EASY_MOBILE_PRO
            switch (submod)
            {
                case Submodule.Contacts:
                    return new List<string>() { FileIO.ToAbsolutePath("EasyMobile/Editor/Templates/Manifests/Manifest_Contacts.xml") };
                case Submodule.Media:
                    return new List<string>() { FileIO.ToAbsolutePath("EasyMobile/Editor/Templates/Manifests/Manifest_CameraGallery.xml") };
                default:
                    return null;                
            }
            #else
            return null;
            #endif
        }

        public override IAndroidPermissionRequired AndroidPermissionHolderForSubmodule(Submodule submod)
        {
            #if EASY_MOBILE_PRO
            switch (submod)
            {
                case Submodule.Media:
                    return EM_Settings.NativeApis.Media as IAndroidPermissionRequired;
                case Submodule.Contacts:
                    return EM_Settings.NativeApis.Contacts as IAndroidPermissionRequired;
                default:
                    return null;
            }
            #else
            return null;
            #endif
        }

        public override IIOSInfoItemRequired iOSInfoItemsHolderForSubmodule(Submodule submod)
        {
            #if EASY_MOBILE_PRO
            switch (submod)
            {
                case Submodule.Media:
                    return EM_Settings.NativeApis.Media as IIOSInfoItemRequired;
                case Submodule.Contacts:
                    return EM_Settings.NativeApis.Contacts as IIOSInfoItemRequired;
                default:
                    return null;
            }
            #else
            return null;
            #endif
        }

        protected override void InternalEnableSubmodule(Submodule submod)
        {
            // Nothing.
        }

        protected override void InternalDisableSubmodule(Submodule submod)
        {
            // Nothing.
        }

        public override List<Submodule> SelfSubmodules
        {
            get
            {
                return new List<Submodule>{ Submodule.Media, Submodule.Contacts };
            }
        }

        #endregion


    }
}
