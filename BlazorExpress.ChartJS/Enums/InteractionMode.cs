namespace BlazorExpress.ChartJS;

public enum InteractionMode
{
    /// <summary>
    /// Finds items in the same dataset.
    /// </summary>
    Dataset,

    /// <summary>
    /// Finds item at the same index. 
    /// </summary>
    Index,

    /// <summary>
    /// Gets the items that are at the nearest distance to the point. 
    /// </summary>
    Nearest,

    /// <summary>
    /// Finds all of the items that intersect the point
    /// </summary>
    Point,

    /// <summary>
    /// Returns all items that would intersect based on the X coordinate of the position only. Would be useful for a vertical cursor implementation. Note that this only applies to cartesian charts.
    /// </summary>
    X,

    /// <summary>
    /// Returns all items that would intersect based on the Y coordinate of the position. This would be useful for a horizontal cursor implementation. Note that this only applies to cartesian charts.
    /// </summary>
    Y
}
