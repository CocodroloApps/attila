using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EasyMobile.Internal
{
    /// <summary>
    /// An abstract class representing objects that act as proxies for unmanaged objects, which are referred to using IntPtrs.
    /// </summary>
    internal abstract class InteropObject : IInteropObject
    {
        private HandleRef mSelfPointer;

        protected abstract void AttachHandle(HandleRef selfPointer);

        protected abstract void ReleaseHandle(HandleRef selfPointer);

        public HandleRef SelfPointer
        {
            get
            {
                if (IsDisposed())
                {
                    throw new InvalidOperationException(
                        "Attempted to use object after it was cleaned up");
                }

                return mSelfPointer;
            }
        }

        public bool IsDisposed()
        {
            return PInvokeUtil.IsNull(mSelfPointer);
        }

        public bool HasSamePointerWith(IInteropObject other)
        {
            return other != null && this.ToPointer().Equals(other.ToPointer());
        }

        protected HandleRef SelfPtr()
        {
            return SelfPointer;
        }

        protected virtual void AttachHandle()
        {
            AttachHandle(SelfPtr());
        }

        protected virtual void ReleaseHandle()
        {
            ReleaseHandle(SelfPtr());
        }

        internal InteropObject(IntPtr pointer)
        {
            mSelfPointer = PInvokeUtil.CheckNonNull(new HandleRef(this, pointer));
            AttachHandle(mSelfPointer);
        }

        ~InteropObject ()
        {
            Cleanup();
        }

        public void Dispose()
        {
            Cleanup();
            System.GC.SuppressFinalize(this);
        }

        public IntPtr ToPointer()
        {
            return SelfPtr().Handle;
        }

        private void Cleanup()
        {
            if (!PInvokeUtil.IsNull(mSelfPointer))
            {
                ReleaseHandle(mSelfPointer);
                mSelfPointer = new HandleRef(this, IntPtr.Zero);
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as InteropObject;

            if (other == null)
                return false;

            if (PInvokeUtil.IsNull(this.mSelfPointer))
                return PInvokeUtil.IsNull(other.mSelfPointer);

            return this.mSelfPointer.Equals(other.mSelfPointer);
        }

        public override int GetHashCode()
        {
            return this.mSelfPointer.GetHashCode();
        }

        public static bool operator ==(InteropObject objA, InteropObject objB)
        {
            if (ReferenceEquals(objA, null))
                return ReferenceEquals(objB, null);

            return objA.Equals(objB);
        }

        public static bool operator !=(InteropObject objA, InteropObject objB)
        {
            return !(objA == objB);
        }
    }
}

