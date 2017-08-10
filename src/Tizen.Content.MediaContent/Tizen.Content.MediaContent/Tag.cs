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
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Tizen.Content.MediaContent
{
    /// <summary>
    /// A Tag is a special piece of information that may be associated with media content items.
    /// Tagging allows a user to organize large number of items into logical groups providing a simplified and faster way of accessing media content items.
    /// </summary>
    public class Tag : ContentCollection
    {
        private IntPtr _tagHandle = IntPtr.Zero;
        private string _tagName = "";

        internal IntPtr Handle
        {
            get
            {
                if (_tagHandle == IntPtr.Zero)
                {
                    throw new ObjectDisposedException(nameof(Tag));
                }

                return _tagHandle;
            }

            set
            {
                _tagHandle = value;
            }
        }
        /// <summary>
        /// The ID of the media tag
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public int Id
        {
            get
            {
                int id;
                MediaContentValidator.ThrowIfError(
                    Interop.Tag.GetTagId(Handle, out id), "Failed to get value");
                return id;
            }
        }

        /// <summary>
        /// The name of the tag
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string Name
        {
            get
            {
                return _tagName;
            }

            set
            {
                MediaContentValidator.ThrowIfError(
                    Interop.Tag.SetName(Handle, value), "Failed to set value");
                _tagName = value;
            }
        }

        internal Tag(IntPtr tagHandle)
        {
            _tagHandle = tagHandle;
            IntPtr val = IntPtr.Zero;
            try
            {
                MediaContentValidator.ThrowIfError(
                    Interop.Tag.GetName(Handle, out val), "Failed to get value");
                _tagName = Marshal.PtrToStringAnsi(val);
            }
            finally
            {
                Interop.Libc.Free(val);
            }
        }

        /// <summary>
        /// Creates a Tag object which can be inserted to the media database using ContentManager:InsertToDatabaseAsync(ContentCollection)
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="tagName">The name of the media tag</param>
        public Tag(string tagName)
        {
            _tagName = tagName;
        }

        /// <summary>
        /// Adds a new media info to the tag.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="mediaContent">The media info which is added</param>
        public void AddItem(MediaInformation mediaContent)
        {
            MediaContentValidator.ThrowIfError(
                Interop.Tag.AddMedia(Handle, mediaContent.MediaId), "Failed to add item");
        }

        /// <summary>
        /// Removes the media info from the given tag.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="mediaContent">The media info which is removed</param>
        public void RemoveItem(MediaInformation mediaContent)
        {
            MediaContentValidator.ThrowIfError(
                Interop.Tag.RemoveMedia(Handle, mediaContent.MediaId), "Failed to remove item");
        }

        /// <summary>
        /// Gets the number of media files for the passed filter in the given tag from the media database.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="filter">ContentFilter used to match media content from the media database.</param>
        /// <returns>The number of media contents matching the filter passed</returns>
        public override int GetMediaInformationCount(ContentFilter filter)
        {
            int mediaCount;
            IntPtr handle = (filter != null) ? filter.Handle : IntPtr.Zero;
            MediaContentValidator.ThrowIfError(
                Interop.Tag.GetMediaCountFromDb(Id, handle, out mediaCount), "Failed to get count");

            return mediaCount;
        }

        public override void Dispose()
        {
            if (_tagHandle != IntPtr.Zero)
            {
                Interop.Tag.Destroy(_tagHandle);
                _tagHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Iterates through media items for a given tag from the media database.
        /// This function gets all media items associated with a given tag and meeting a desired filter.
        /// If NULL is passed to the filter, no filtering is applied.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="filter">ContentFilter used to match media content from the media database.</param>
        /// <returns>List of content media items matching the passed filter</returns>
        public override IEnumerable<MediaInformation> GetMediaInformations(ContentFilter filter)
        {
            List<MediaInformation> mediaContents = new List<MediaInformation>();
            IntPtr handle = (filter != null) ? filter.Handle : IntPtr.Zero;

            Interop.Tag.MediaInfoCallback callback = (IntPtr mediaHandle, IntPtr data) =>
            {
                Interop.MediaInformation.SafeMediaInformationHandle newHandle;
                MediaContentValidator.ThrowIfError(
                    Interop.MediaInformation.Clone(out newHandle, mediaHandle), "Failed to clone media");

                MediaContentType type;
                Interop.MediaInformation.GetMediaType(newHandle, out type);
                if (type == MediaContentType.Image)
                {
                    Interop.ImageInformation.SafeImageInformationHandle imageInfo;
                    MediaContentValidator.ThrowIfError(
                        Interop.MediaInformation.GetImage(mediaHandle, out imageInfo), "Failed to get image information");

                    mediaContents.Add(new ImageInformation(imageInfo, newHandle));
                }
                else if ((type == MediaContentType.Music) || (type == MediaContentType.Sound))
                {
                    Interop.AudioInformation.SafeAudioInformationHandle audioInfo;
                    MediaContentValidator.ThrowIfError(
                        Interop.MediaInformation.GetAudio(mediaHandle, out audioInfo), "Failed to get audio information");

                    mediaContents.Add(new AudioInformation(audioInfo, newHandle));
                }
                else if (type == MediaContentType.Video)
                {
                    Interop.VideoInformation.SafeVideoInformationHandle videoInfo;
                    MediaContentValidator.ThrowIfError(
                        Interop.MediaInformation.GetVideo(mediaHandle, out videoInfo), "Failed to get video information");

                    mediaContents.Add(new VideoInformation(videoInfo, newHandle));
                }
                else if (type == MediaContentType.Others)
                {
                    mediaContents.Add(new MediaInformation(newHandle));
                }

                return true;
            };

            MediaContentValidator.ThrowIfError(
                Interop.Tag.ForeachMediaFromDb(Id, handle, callback, IntPtr.Zero), "Failed to get information");

            return mediaContents;
        }
    }
}