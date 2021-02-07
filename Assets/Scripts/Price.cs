public class Price
{
    public struct Order
    {
        public int Legth { get; set; }

        public int Price { get; set; }
    }

    private Order[] length = new Order[]
    {
        new Order{Legth = 10, Price = 100},
        new Order{Legth = 20, Price = 210},
        new Order{Legth = 30, Price = 350},
        new Order{Legth = 40, Price = 520},
        new Order{Legth = 50, Price = 700},
        new Order{Legth = 60, Price = 900},
        new Order{Legth = 70, Price = 1180},
        new Order{Legth = 80, Price = 1400},
        new Order{Legth = 90, Price = 1800},
        new Order{Legth = 100, Price = 2500}
    };

    private Order[] strength = new Order[]
    {
        new Order{Legth = 3, Price = 100},
        new Order{Legth = 4, Price = 210},
        new Order{Legth = 5, Price = 350},
        new Order{Legth = 6, Price = 520},
        new Order{Legth = 7, Price = 700},
        new Order{Legth = 8, Price = 900},
        new Order{Legth = 9, Price = 1180},
        new Order{Legth = 10, Price = 1400},
        new Order{Legth = 11, Price = 1800},
        new Order{Legth = 12, Price = 2500}
    };

    public int Length => length[PlayerProgress.OrderIndexLength].Legth;

    public int Strength => strength[PlayerProgress.OrderIndexStrength].Legth;

    public int NextLength => (PlayerProgress.OrderIndexLength + 1) < length.Length ? 
                                    length[PlayerProgress.OrderIndexLength + 1].Legth :
                                    length[PlayerProgress.OrderIndexLength].Legth;
    public int NextLengthPrice => (PlayerProgress.OrderIndexLength + 1) < length.Length ?
                                    length[PlayerProgress.OrderIndexLength + 1].Price :
                                    length[PlayerProgress.OrderIndexLength].Price;

    public int NextStrength => (PlayerProgress.OrderIndexStrength + 1) < strength.Length ?
                                    strength[PlayerProgress.OrderIndexStrength + 1].Legth :
                                    strength[PlayerProgress.OrderIndexStrength].Legth;
    public int NextStrengthPrice => (PlayerProgress.OrderIndexStrength + 1) < strength.Length ?
                                     strength[PlayerProgress.OrderIndexStrength + 1].Price:
                                     strength[PlayerProgress.OrderIndexStrength].Price;     

    public bool HasBuyLength()
    {
        if(PlayerProgress.OrderIndexLength + 1 >= length.Length)
        {
            return false;
        }

        return PlayerProgress.Money >= length[PlayerProgress.OrderIndexLength + 1].Price;
    }
    public bool HasBuyStrength()
    {
        if (PlayerProgress.OrderIndexStrength + 1 >= strength.Length)
        {
            return false;
        }

        return PlayerProgress.Money >= strength[PlayerProgress.OrderIndexStrength + 1].Price;
    }

    public void IncrementeIndexOfLength()
    {
        if (PlayerProgress.OrderIndexLength + 1 >= length.Length)
        {
            return;
        }
        ++PlayerProgress.OrderIndexLength;
    }

    public void IncrementeIndexOfStrength()
    {
        if (PlayerProgress.OrderIndexStrength + 1 >= strength.Length)
        {
            return;
        }
        ++PlayerProgress.OrderIndexStrength;
    }
}

