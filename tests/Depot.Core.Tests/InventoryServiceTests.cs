using Depot.Core;
using Xunit;

namespace Depot.Core.Tests;

public class InventoryServiceTests
{
    private static InventoryService WithBolts()
    {
        var inventory = new InventoryService();
        inventory.AddProduct(new Product("BOLT-M8", "M8 hex bolt, zinc", "each"));
        return inventory;
    }

    [Fact]
    public void Receiving_stock_increases_what_is_on_hand()
    {
        var inventory = WithBolts();

        inventory.Receive("BOLT-M8", 250);

        Assert.Equal(250, inventory.OnHand("BOLT-M8"));
    }

    [Fact]
    public void Shipping_stock_decreases_what_is_on_hand()
    {
        var inventory = WithBolts();
        inventory.Receive("BOLT-M8", 250);

        inventory.Ship("BOLT-M8", 50);

        Assert.Equal(200, inventory.OnHand("BOLT-M8"));
    }

    [Fact]
    public void Shipping_more_than_is_on_hand_is_refused()
    {
        var inventory = WithBolts();
        inventory.Receive("BOLT-M8", 10);

        var error = Assert.Throws<InsufficientStockException>(
            () => inventory.Ship("BOLT-M8", 11));

        Assert.Equal(10, error.OnHand);
        Assert.Equal(11, error.Requested);
        Assert.Equal(10, inventory.OnHand("BOLT-M8"));  // nothing moved
    }
}