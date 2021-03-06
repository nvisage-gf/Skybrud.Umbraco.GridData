﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the embed value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlEmbedValue : GridControlHtmlValue {
        
        #region Constructors

        protected GridControlEmbedValue(GridControl control, JToken token) : base(control, token) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an embed value from the specified <code>JToken</code>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <code>JToken</code> to be parsed.</param>
        public new static GridControlEmbedValue Parse(GridControl control, JToken token) {
            return token == null ? null : new GridControlEmbedValue(control, token);
        }

        #endregion

    }

}