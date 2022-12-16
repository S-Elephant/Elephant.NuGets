# About

Contains Controller helpers.

## ElephantControllerBase

```c#
protected IActionResult ToApiResult<T>(ResponseWrapper<T> result) where T : new();
protected IActionResult CreatedResult();
```

## ControllerExtensions

```c#
public static byte[] ToByteArray(this IFormFile formFile);
public static IFormFile ToIFormFile(this byte[] byteArray, string name = "", string filename = "", string contentType = "");
```

