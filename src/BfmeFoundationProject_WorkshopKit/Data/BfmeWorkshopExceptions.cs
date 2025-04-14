namespace BfmeFoundationProject.WorkshopKit.Data
{
    public class BfmeWorkshopPackageNotSyncableException : Exception
    {
        public BfmeWorkshopPackageNotSyncableException(string message) : base(message) { }
    }

    public class BfmeWorkshopGameNotInstalledException : Exception
    {
        public BfmeWorkshopGameNotInstalledException(string message) : base(message) { }
    }

    public class BfmeWorkshopEnhancementIncompatibleException : Exception
    {
        public BfmeWorkshopEnhancementIncompatibleException(string message) : base(message) { }
    }

    public class BfmeWorkshopFileMissingException : Exception
    {
        public BfmeWorkshopFileMissingException(string message) : base(message) { }
    }

    public class BfmeWorkshopEntryNotFoundException : Exception
    {
        public BfmeWorkshopEntryNotFoundException(string message) : base(message) { }
    }

    public class BfmeWorkshopScriptMissingRequirementsException : Exception
    {
        public BfmeWorkshopScriptMissingRequirementsException(string message) : base(message) { }
    }

    public class BfmeWorkshopScriptSyntaxErrorException : Exception
    {
        public BfmeWorkshopScriptSyntaxErrorException(string message) : base(message) { }
    }
}
