public class ImplementedGlobalInputEvents
{
    public IGlobalClick click => _click;
    public IGlobalClickDown clickDown => _clickDown;
    public IGlobalClickUp clickUp => _clickUp;
    public IGlobalClickHold clickHold => _clickHold;
    public IGlobalClickDrag clickDrag => _clickDrag;
    public IGlobalSwipe swipe => _swipe;
    
    private readonly IGlobalClick _click;
    private readonly IGlobalClickDown _clickDown;
    private readonly IGlobalClickUp _clickUp;
    private readonly IGlobalClickHold _clickHold;
    private readonly IGlobalClickDrag _clickDrag;
    private readonly IGlobalSwipe _swipe;

    public ImplementedGlobalInputEvents(IGlobalClick click, IGlobalClickDown clickDown, IGlobalClickUp clickUp, IGlobalClickHold clickHold,
        IGlobalClickDrag clickDrag, IGlobalSwipe swipe)
    {
        _click = click;
        _clickDown = clickDown;
        _clickUp = clickUp;
        _clickHold = clickHold;
        _clickDrag = clickDrag;
        _swipe = swipe;
    }
}