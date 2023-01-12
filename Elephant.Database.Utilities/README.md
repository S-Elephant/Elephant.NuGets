# About

Contains database utility methods without having database dependencies.

# IdService

May be used to check if a model is an insert or update model.

```c#
bool IsIdInsert(int id);
bool IsIdUpdate(int id);
bool IsIdNotInsert(int id);
bool IsIdNotUpdate(int id);
bool IsInvalid(int id);
bool IsValid(int id);
```