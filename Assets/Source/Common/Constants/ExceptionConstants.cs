namespace Source.Common.Constants
{
    public static class ExceptionConstants
    {
        public const string FailedToAddListener =
            "Attempting to add listener with inconsistent signature for event type '{0}'. Existing listener type: {1}. Added listener has type: {2}";

        public const string FailedToRemoveListener =
            "Attempting to remove listener with inconsistent signature for event type '{0}'. Existing listener have type: {1}. Removed listener has type: {2}";

        public const string CanNotResolveService = "Can't resolve service of type: '{0}'";
        public const string CanNotResolveDescriptor = "Can't resolve descriptor for type: '{0}'";
        public const string CanNotRegisterDescriptor = "Can't register descriptor with null Instance. Service type: '{0}'";
    }
}