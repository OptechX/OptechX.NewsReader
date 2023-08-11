# OptechX.NewsReader

OptechX.NewsReader is a console application that fetches news articles based on the provided parameters and interacts with a news API.

## Instructions

To run the OptechX.NewsReader application, follow these steps:

1. **Prerequisites**: Make sure you have [.NET SDK](https://dotnet.microsoft.com/download/dotnet) installed on your machine.

2. **Clone the Repository**: Clone or download this repository to your local machine.

3. **Navigate to the Project Directory**: Open a terminal or command prompt and navigate to the directory where you have cloned/downloaded the OptechX.NewsReader project.

4. **Build the Application**: Run the following command to build the application:

```bash
dotnet build
```

5. **Run the Application**: To run the application, use the following command with the required arguments:

```bash
dotnet run --apiKey <api_key> --country <country> --category <category> --pageSize <page_size>
```

## Notes

Replace `<api_key>`, `<country>`, `<category>`, and `<page_size>` with the appropriate values. For example:

```bash
dotnet run --apiKey YOUR_API_KEY --country us --category technology --pageSize 10
```

- `apiKey`: Your API key for accessing the news API.
- `country`: The country for which you want to retrieve news articles (e.g., us, gb, etc.).
- `category`: The category of news articles (e.g., technology, business, etc.).
- `pageSize`: The number of articles to retrieve.

Build Self-contained Executable: To build a self-contained executable, run the following command:

```bash
dotnet publish -c Release /p:PublishSingleFile=true --self-contained
```

This will generate an executable in the bin/Release/netX.0/publish directory.

**License:** This project is licensed under the [MIT License](https://opensource.org/license/mit/).
