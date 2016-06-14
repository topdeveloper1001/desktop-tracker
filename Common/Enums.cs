public enum State
{
    INITIAL,
    STARTED,
    STOPPED,
    UNKNOWN
}

public enum LoginState
{
    LOGGED,
    LOGIN_FAIL,
    CONNECTION_FAIL,
    NOT_LOGGED
}

public enum VersionState
{
    LATEST,
    UPDATE_AVAILABLE,
    UPDATE_REQUIRED,
    UNKNOWN
}

public enum LogDataType
{
    IN_MEMORY,
    FILE,
    UNKNOWN
}

public enum ClearCacheState
{
    SUCCESS,
    EMPTY_CACHE,
    ERROR,
    UNKNOWN
}