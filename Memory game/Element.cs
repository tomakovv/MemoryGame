

namespace Memory_game
{
    public enum ElementState
    {
        Hidden, Revealed, Matched
    }
    public class Element
    {
        public Guid Id { get;}
        public string Back { get;  }
        public string Front { get; }
        public ElementState State { get; private set; }

        public Element(string name)
        {
            Id = Guid.NewGuid();
            Back = name;
            Front = "x";
            State = ElementState.Hidden;
        }

        public void SetStatus(ElementState state)
        {
            State = state;
        }
        public string ShowElement()
        {
            return State switch
            {
                ElementState.Hidden => Front,
                ElementState.Revealed => Back,
                ElementState.Matched => Back,
            };
        }
    }


}
