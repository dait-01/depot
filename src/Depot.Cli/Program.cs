using Depot.Core;

var inventory = new InventoryService();

inventory.AddProduct(new Product("BOLT-M8", "M8 hex bolt, zinc", "each"));
inventory.AddProduct(new Product("CABLE-CAT6", "Cat6 patch cable, 2 m", "each"));
inventory.AddProduct(new Product("OIL-15W40", "Engine oil 15W-40", "litre"));

inventory.Receive("BOLT-M8", 500);
inventory.Receive("CABLE-CAT6", 40);
inventory.Receive("OIL-15W40", 120);

inventory.Ship("CABLE-CAT6", 12);

Console.WriteLine();
Console.WriteLine("SKU          DESCRIPTION               ON HAND  UNIT");
Console.WriteLine(new string('-', 56));

foreach (var line in inventory.Report())
{
    Console.WriteLine(
        $"{line.Product.Sku,-12} {line.Product.Name,-24} {line.Quantity,8}  {line.Product.Unit}");
}

Console.WriteLine();
