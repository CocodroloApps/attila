using System;
using EasyMobile.Internal.NativeAPIs.Media;

namespace EasyMobile
{
    /// <summary>
    /// Entry class for device camera & gallery APIs.
    /// </summary>
    public static class Media
    {
        /// <summary>
        /// Entry interface to use native device's Camera APIs.
        /// </summary>
        public static IDeviceCamera Camera;

        /// <summary>
        /// Entry interface to use native device's Gallery APIs.
        /// </summary>
        public static IDeviceGallery Gallery;

        static Media()
        {
            Camera = GetCamera();
            Gallery = GetGallery();
        }

        private static IDeviceCamera GetCamera()
        {
            #if UNITY_EDITOR
            return new UnsupportedDeviceCamera();
            #elif UNITY_ANDROID
            return new AndroidDeviceCamera();
            #elif UNITY_IOS
            return new iOSDeviceCamera();
            #else
            return new UnsupportedDeviceCamera();
            #endif
        }

        private static IDeviceGallery GetGallery()
        {
            #if UNITY_EDITOR
            return new UnsupportedDeviceGallery();
            #elif UNITY_ANDROID
            return new AndroidDeviceGallery();
            #elif UNITY_IOS
            return new iOSDeviceGallery();
            #else
            return new UnsupportedDeviceGallery();
            #endif
        }
    }
}
