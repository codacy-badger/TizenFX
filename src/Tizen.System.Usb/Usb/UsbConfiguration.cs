﻿/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using System;
using System.Collections.Generic;

namespace Tizen.System.Usb
{
    /// <summary>
    /// Class to manage USB Configuration.
    /// </summary>
    public class UsbConfiguration : IDisposable
    {
        internal readonly Interop.UsbConfigHandle _handle;
        private readonly UsbDevice _parent;
        private Dictionary<int, UsbInterface> _interfaces;

        internal UsbConfiguration(UsbDevice parent, Interop.UsbConfigHandle handle)
        {
            _parent = parent;
            _handle = handle;
        }

        /// <summary>
        /// Checks if device is self-powered in given configuration.
        /// </summary>
        public bool IsSelfPowered
        {
            get
            {
                ThrowIfDisposed();
                return Interop.NativeGet<bool>(_handle.IsSelfPowered);
            }
        }

        /// <summary>
        /// Checks if device in given configuration supports remote wakeup.
        /// </summary>
        public bool SupportRemoteWakeup
        {
            get
            {
                ThrowIfDisposed();
                return Interop.NativeGet<bool>(_handle.SupportRemoteWakeup);
            }
        }

        /// <summary>
        /// Gets maximum power required in given configuration, in mA.
        /// </summary>
        public int MaximumPowerRequired
        {
            get
            {
                ThrowIfDisposed();
                return Interop.NativeGet<int>(_handle.GetMaxPower);
            }
        }

        /// <summary>
        /// Dictionary mapping interfaces Ids to interface instances for given configuration.
        /// </summary>
        public IReadOnlyDictionary<int, UsbInterface> Interfaces
        {
            get
            {
                ThrowIfDisposed();
                if (_interfaces == null)
                {
                    _interfaces = new Dictionary<int, UsbInterface>();
                    int count = Interop.NativeGet<int>(_handle.GetNumInterfaces);
                    for (int i = 0; i < count; ++i)
                    {
                        Interop.UsbInterfaceHandle handle;
                        _handle.GetInterface(i, out handle);
                        UsbInterface usbInterface = new UsbInterface(this, handle);
                        _interfaces.Add(usbInterface.Id, usbInterface);
                    }
                }
                return _interfaces;
            }
        }

        /// <summary>
        /// Configuration string.
        /// </summary>
        public string ConfigurationString
        {
            get
            {
                ThrowIfDisposed();
                return Interop.DescriptorString(_handle.GetConfigStr);
            }
        }

        /// <summary>
        /// Set this configuration as active configuration for the device.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throws exception if device is disconnected or not opened for operation or busy as its interfaces are currently claimed.
        /// </exception>
        public void SetAsActive()
        {
            ThrowIfDisposed();
            _handle.SetAsActive();
        }

        internal void ThrowIfDisposed()
        {
            if (disposedValue) throw new ObjectDisposedException("Configuration is already disposed");
            _parent.ThrowIfDisposed();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _handle.Dispose();
                disposedValue = true;
            }
        }

        ~UsbConfiguration()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}