namespace Depot.Core;

public class InventoryService
{
    private readonly Dictionary<string, Product> _catalog = new();
    private readonly Dictionary<string, int> _onHand = new();

    public void AddProduct(Product product)
    {
        _catalog[product.Sku] = product;

        if (!_onHand.ContainsKey(product.Sku))
        {
            _onHand[product.Sku] = 0;
        }
    }

    public int OnHand(string sku)
    {
        if (!_onHand.TryGetValue(sku, out var quantity))
        {
            throw new UnknownSkuException(sku);
        }

        return quantity;
    }

    public void Receive(string sku, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity), "Receive quantity must be positive.");
        }

        _onHand[sku] = OnHand(sku) + quantity;
    }

    public void Ship(string sku, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity), "Ship quantity must be positive.");
        }

        var current = OnHand(sku);

        if (quantity > current)
        {
            throw new InsufficientStockException(sku, current, quantity);
        }

        _onHand[sku] = current - quantity;
    }

    public IReadOnlyList<StockLine> Report()
    {
        return _catalog.Values
            .OrderBy(product => product.Sku)
            .Select(product => new StockLine(product, _onHand[product.Sku]))
            .ToList();
    }
}