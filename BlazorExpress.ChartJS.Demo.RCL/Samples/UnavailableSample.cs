namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// A Chart.js sample the library cannot express yet. The gallery shows it as a disabled card; it has no page.
/// </summary>
/// <param name="Title">The Chart.js sample name.</param>
/// <param name="Description">What the sample shows.</param>
/// <param name="Category">The gallery group.</param>
/// <param name="WaitingFor">The library capability the sample needs.</param>
public sealed record UnavailableSample(string Title, string Description, SampleCategory Category, string WaitingFor);
