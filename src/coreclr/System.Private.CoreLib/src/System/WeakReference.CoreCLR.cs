// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace System
{
    public partial class WeakReference : ISerializable
    {
        // If you fix bugs here, please fix them in WeakReference<T> at the same time.

        // This field is not a regular GC handle. It can have a special values that are used to prevent a race condition between setting the target and finalization.
        internal IntPtr m_handle;

        // Determines whether or not this instance of WeakReference still refers to an object
        // that has not been collected.
        //
        public extern virtual bool IsAlive
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        // Gets the Object stored in the handle if it's accessible.
        // Or sets it.
        //
        public extern virtual object? Target
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }

        // Free all system resources associated with this reference.
        //
        // Note: The WeakReference finalizer is not actually run, but
        // treated specially in gc.cpp's ScanForFinalization
        // This is needed for subclasses deriving from WeakReference, however.
        // Additionally, there may be some cases during shutdown when we run this finalizer.
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern ~WeakReference();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void Create(object? target, bool trackResurrection);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern bool IsTrackResurrection();
    }
}
