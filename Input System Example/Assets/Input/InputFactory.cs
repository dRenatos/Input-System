using UnityEngine;

public static class InputFactory 
{
    public static BaseInput Create()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            var androidInputSetup = Resources.Load<InputSetup>("Android Setup");

            if (!androidInputSetup)
            {
                Debug.LogError("Haven't found a input setup for android. Please create at resources folder a input setup file with name Android Setup");
            }

            return new AndroidInput(androidInputSetup.minSwipeLength, androidInputSetup.minSwipeTime, androidInputSetup.maxSwipeTime);
        }

        var editorInputSetup = Resources.Load<InputSetup>("Editor Setup");

        if (!editorInputSetup)
        {
            Debug.LogError("Haven't found a input setup for editor. Please create at resources folder a input setup file with name Editor Setup");
        }
        
        return new EditorInput(editorInputSetup.minSwipeLength, editorInputSetup.minSwipeTime, editorInputSetup.maxSwipeTime);
    }
}
