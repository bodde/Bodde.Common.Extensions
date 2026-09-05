# New Extensions
- [x] string? - OrEmpty
- [x] string? - IsNotNullOrEmpty
- [x] string? - IsNotNullOrWhitespace
- [x] T[]? - IsNullOrEmpty
- [x] T[]? - IsNotNullOrEmpty
- [x] add NotNullWhen(false) attribute for IsNullOrEmpty and IsNullOrWhiteSpace
- [x] add NotNullWhen(true) attribute for IsNotNullOrEmpty and IsNotNullOrWhiteSpace
- [ ] Type - IsCollection ((valueType.GetElementType() ?? valueType.GetGenericArguments().FirstOrDefault()) != null)
- [ ] Regex - MatchSingle<T> returning a T instance with property values matching single named groups
- [ ] Regex - MatchFirst<T> returning a T instance with property values matching first named groups
- [ ] Regex - MatchAll<T> returning an array of T instances with property values matching all named groups

# Publish
- [ ] New tag release
- [ ] Publish to Nuget
