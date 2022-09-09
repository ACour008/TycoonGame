using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    public class grid_test
    {
        [Test]
        public void grid_has_one_row()
        {
            // Arrange
            GameGrid<int> grid = new GameGrid<int>(1, 1, new Vector2(64, 64));

            // Act

            // Assert
            Assert.AreEqual(1, grid.Height);
        }

        [Test]
        public void grid_has_one_column()
        {
            GameGrid<int> grid = new GameGrid<int>(1, 1, new Vector2(64, 64));
            Assert.AreEqual(1, grid.Width);
        }

        [Test]
        public void grid_cell_size_is_64()
        {
            GameGrid<int> grid = new GameGrid<int>(1, 1, new Vector2(64, 64));
            Assert.AreEqual(64, grid.CellSize);
        }

        [Test]
        public void data_at_coordinate_0_0_is_128()
        {
            // Arrange
            GameGrid<int> grid = new GameGrid<int>(20, 20, new Vector2(64, 64));

            // Act;
            grid.SetValueAt(0, 0, 128);

            // Assert
            Assert.AreEqual(128, grid.GetValueAt(0, 0));
        }
    }

}
