namespace EcoModel
{
    class Prey : Cell
    {
        public int TimeToReproduce { get; protected set; }

        public bool Move { get; private set; }

        public Prey(Ocean AOcean, Coordinate aCoord, bool move,
            int timeToReproduce = Constants.DEFAULT_TIME_TO_REPRODUCE,
            char image = (char)CellsSymbols.PreyImage)
            : base(AOcean, aCoord, image)
        {
            Move = move;
            TimeToReproduce = timeToReproduce;
        }

        public override void Process()
        {
            Coordinate toCoord = GetNeighborCoord
                    ((char)CellsSymbols.DefaultImage);

            if (!Move)
            {
                TimeToReproduce--;
            }

            if (_offset != toCoord && !Move)
            {
                _owner.MovePrey(_offset, toCoord, TimeToReproduce);
            }
        }

        protected virtual void Reproduce(Coordinate anOffset)
        {
        }

        public void FixEndOfIteration()
        {
            Move = false;
        }
    }
}
