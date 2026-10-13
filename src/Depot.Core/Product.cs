namespace Depot.Core;

// A record is a class whose whole identity is its values.
// Two products with the same SKU, name and unit are the same product.
public record Product(string Sku, string Name, string Unit);

public record StockLine(Product Product, int Quantity);