namespace Depot.Core;

public class UnknownSkuException : Exception
{
    public UnknownSkuException(string sku)
        : base($"No product with SKU '{sku}' is in the catalog.")
    {
        Sku = sku;
    }

    public string Sku { get; }
}

public class InsufficientStockException : Exception
{
    public InsufficientStockException(string sku, int onHand, int requested)
        : base($"Cannot ship {requested} of '{sku}': only {onHand} on hand.")
    {
        Sku = sku;
        OnHand = onHand;
        Requested = requested;
    }

    public string Sku { get; }
    public int OnHand { get; }
    public int Requested { get; }
}