namespace EcoModel
{
    class Predator : Prey
    {
        public int TimeToFeed { get; private set; }

        public Predator(Ocean AOcean, Coordinate offset, bool move,
                int timeToReproduse = Constants.DEFAULT_TIME_TO_REPRODUCE,
                int timeToFeed = Constants.DEFAULT_TIME_TO_FEED)
                : base(AOcean, offset, move, timeToReproduse, (char)CellsSymbols.PredatorImage)
        {
            TimeToFeed = timeToFeed;
        }

        public override void Process()
        {
            bool eaten = false;

            Coordinate toCoord = GetNeighborCoord
                    ((char)CellsSymbols.PreyImage);

            //if (_offset == toCoord)
            //{
            //    toCoord = GetNeighborCoord((char)CellsSymbols.PredatorImage);

                if (_offset == toCoord)
                {
                    toCoord = GetNeighborCoord((char)CellsSymbols.DefaultImage);
                    eaten = false;
                }
                else
                {
                    eaten = true;
                }
            //}
            //else
            //{
            //    eaten = true;
            //}

            if (!Move)
            {
                TimeToReproduce--;
                TimeToFeed--;
            }

            if (_offset != toCoord && !Move)
            {
                _owner.MovePredator(_offset, toCoord, TimeToReproduce, TimeToFeed, eaten);
            }
        }

        protected override void Reproduce(Coordinate anOffset)
        {
        }
    }
}
