﻿using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData.Interfaces {

    /// <summary>
    /// Interface describing a grid control value.
    /// </summary>
    public interface IGridControlValue {

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        [JsonIgnore]
        GridControl Control { get; }

    }

}