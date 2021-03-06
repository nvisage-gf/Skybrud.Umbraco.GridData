﻿using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing an area in an Umbraco Grid.
    /// </summary>
    public class GridArea : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridRow</code>.
        /// </summary>
        [JsonIgnore]
        public GridRow Row { get; private set; }

        /// <summary>
        /// Gets the column width of the area.
        /// </summary>
        public int Grid { get; private set; }

        /// <summary>
        /// Gets wether all editors are allowed for this area.
        /// </summary>
        public bool AllowAll { get; private set; }

        /// <summary>
        /// Gets an array of all editors allowed for this area. If <code>AllowAll</code> is <code>TRUE</code>, this
        /// array may be empty.
        /// </summary>
        public string[] Allowed { get; private set; }

        /// <summary>
        /// Gets an array of all controls added to this area.
        /// </summary>
        public GridControl[] Controls { get; private set; }
        
        /// <summary>
        /// Gets a dictionary representing the styles of the area.
        /// </summary>
        public GridDictionary Styles { get; private set; }

        /// <summary>
        /// Gets a dictionary representing the configuration (called Settings in the backoffice) of the area.
        /// </summary>
        public GridDictionary Config { get; private set; }

        /// <summary>
        /// Gets a reference to the previous area.
        /// </summary>
        public GridArea PreviousArea { get; internal set; }

        /// <summary>
        /// Gets a reference to the next area.
        /// </summary>
        public GridArea NextArea { get; internal set; }

        /// <summary>
        /// Gets whether the area has any controls.
        /// </summary>
        public bool HasControls {
            get { return Controls.Length > 0; }
        }

        /// <summary>
        /// Gets the first control of the area. If the area doesn't contain
        /// any controls, this property will return <code>NULL</code>.
        /// </summary>
        public GridControl FirstControl {
            get { return Controls.FirstOrDefault(); }
        }

        /// <summary>
        /// Gets the last control of the area. If the area doesn't contain
        /// any controls, this property will return <code>NULL</code>.
        /// </summary>
        public GridControl LastControl {
            get { return Controls.LastOrDefault(); }
        }

        #endregion

        #region Constructors

        protected GridArea(JObject obj) : base(obj) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses an area from the specified <code>obj</code>.
        /// </summary>
        /// <param name="row">The parent row of the area.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridArea Parse(GridRow row, JObject obj) {

            // Some input validation
            if (obj == null) throw new ArgumentNullException("obj");
            
            // Parse the array of allow blocks
            JArray allowed = obj.GetArray("allowed");
            
            // Parse basic properties
            GridArea area = new GridArea(obj) {
                Row = row,
                Grid = obj.GetInt32("grid"),
                AllowAll = obj.GetBoolean("allowAll"),
                Allowed = allowed == null ? new string[0] : allowed.Select(x => (string)x).ToArray(),
                Styles = obj.GetObject("styles", GridDictionary.Parse),
                Config = obj.GetObject("config", GridDictionary.Parse)
            };

            // Parse the controls
            area.Controls = obj.GetArray("controls", x => GridControl.Parse(area, x)) ?? new GridControl[0];
            
            // Update "PreviousArea" and "NextArea" properties
            for (int i = 1; i < area.Controls.Length; i++) {
                area.Controls[i - 1].NextControl = area.Controls[i];
                area.Controls[i].PreviousControl = area.Controls[i - 1];
            }
            
            // Return the row
            return area;
        
        }

        #endregion

    }

}