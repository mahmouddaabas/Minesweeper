using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest;

[TestClass]
public class Tests
{
    [TestMethod]
    public void Test_Set_Bomb_And_CheckInputCoords_Success()
    {
        Minefield field = new Minefield();
        field.SetBomb(0, 0);

        bool result = field.CheckInputCoords(0, 0);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Test_Set_Bomb_And_CheckInputCoords_Fail()
    {
        Minefield field = new Minefield();
        field.SetBomb(4, 0);

        bool result = field.CheckInputCoords(0, 0);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CountAdjacentBombs_NoBombsAround_ReturnsZero()
    {
        Minefield field = new Minefield();
        field.SetBomb(0, 0);

        int bombCount = field.CountAdjacentBombs(2, 2);

        Assert.AreEqual(0, bombCount);
    }

    [TestMethod]
    public void CountAdjacentBombs_BombsAround_ReturnsNonZero()
    {
        Minefield field = new Minefield();
        field.SetBomb(0, 0);

        int bombCount = field.CountAdjacentBombs(1, 1);

        Assert.IsTrue(bombCount > 0);
    }

    [TestMethod]
    public void CheckWin_Success()
    {
        Minefield field = new Minefield();
        field.setClearedFieldCounter(20);

        bool result = field.CheckWin();

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CheckWin_Fail()
    {
        Minefield field = new Minefield();
        field.setClearedFieldCounter(18);

        bool result = field.CheckWin();

        Assert.IsFalse(result);
    }
}
