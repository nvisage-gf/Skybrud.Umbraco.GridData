﻿using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the HTML value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlHtmlValue : GridControlTextValue {

        #region Properties

        /// <summary>
        /// Gets an instance of <code>HtmlString</code> representing the text value.
        /// </summary>
        [JsonIgnore]
        public HtmlString HtmlValue { get; private set; }

        #endregion

        #region Constructors

        protected GridControlHtmlValue(GridControl control, JToken token) : base(control, token) {
            HtmlValue = new HtmlString(Value);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a text value from the specified <code>JToken</code>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <code>JToken</code> to be parsed.</param>
        public new static GridControlTextValue Parse(GridControl control, JToken token) {
            return token == null ? null : new GridControlHtmlValue(control, token);
        }

        #endregion
        
    }

}