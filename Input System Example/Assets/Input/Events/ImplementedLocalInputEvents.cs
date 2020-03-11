public class ImplementedLocalInputEvents
{
    public ILocalClick click => _click;
    public ILocalClickDown clickDown => _clickDown;
    public ILocalClickUp clickUp => _clickUp;
    public ILocalClickHold clickHold => _clickHold;
    public ILocalClickDrag clickDrag => _clickDrag;
    public ILocalSwipe swipe => _swipe; 
    
    private readonly ILocalClick _click;
    private readonly ILocalClickDown _clickDown;
    private readonly ILocalClickUp _clickUp;
    private readonly ILocalClickHold _clickHold;
    private readonly ILocalClickDrag _clickDrag;
    private readonly ILocalSwipe _swipe;

    public ImplementedLocalInputEvents(ILocalClick click, ILocalClickDown clickDown, ILocalClickUp clickUp, ILocalClickHold clickHold,
        ILocalClickDrag clickDrag, ILocalSwipe swipe)
    {
        _click = click;
        _clickDown = clickDown;
        _clickUp = clickUp;
        _clickHold = clickHold;
        _clickDrag = clickDrag;
        _swipe = swipe;
    }
}
