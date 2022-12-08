using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class Move
    {
        GameObject piece;
        private int fromX;
        private int fromY;
        private int toX;
        private int toY;

        public Move(GameObject piece, int fromX, int fromY, int toX, int toY) {
            this.piece = piece;
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
        }

        public GameObject getPiece() {
            return piece;
        }

        public int getFromX()
        {
            return fromX;
        }

        public int getFromY()
        {
            return fromY;
        }

        public int getToX()
        {
            return toX;
        }

        public int getToY()
        {
            return toY;
        }
    }
}
