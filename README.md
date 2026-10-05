# DocuScope

A lightweight, localized web search engine built from scratch in **C# / .NET 8** and **ASP.NET Core**.

DocuScope fetches any webpage (such as Wikipedia articles or research papers), extracts clean textual content, builds an in-memory **inverted index**, and allows instant keyword querying to return matching paragraphs rather than full pages.

---

## Features

- **Asynchronous Web Scraper**: Fetches web content reliably using `HttpClient` with custom request headers.
- **HTML Document Extractor**: Cleans and parses HTML nodes with `HtmlAgilityPack` and XPath, filtering out scripts, styles, and empty elements.
- **Inverted Index Engine**: High-performance $O(1)$ keyword-to-paragraph mapping (`Dictionary<string, HashSet<int>>`) with full text normalization (casing, punctuation stripping, deduplication).
- **Dual Presentation Layers**:
  - **CLI Runner**: Interactive terminal client for rapid local search.
  - **ASP.NET Core Web API**: Minimal API backend serving a responsive browser UI (`index.html` + vanilla JS `fetch`).

---

## Project Structure

```text
DocuScope/
├── src/
│   ├── DocuScope.Core/       # Class Library: WebScraper, HtmlExtractor, InvertedIndex, SearchEngine
│   ├── DocuScope.Cli/        # Console App: Interactive terminal runner
│   └── DocuScope.Web/        # ASP.NET Core Web API & static web frontend (wwwroot/index.html)
└── DocuScope.sln             # .NET Solution file
```

---

## Quickstart

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### 1. Run the Web Application
```bash
dotnet run --project src/DocuScope.Web
```
Open the printed local URL (e.g., `http://localhost:5000`) in your browser.

### 2. Run the CLI Runner
```bash
dotnet run --project src/DocuScope.Cli
```
Enter a URL and start searching keywords directly from your terminal.

---

## License
MIT License. Created by [Muhammad Soban Saqib](LICENSE).
