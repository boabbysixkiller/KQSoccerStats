using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using SoccerStats.Classes;

namespace SoccerStats.Helper
{
    public static class SoccerStatsWorker
    {
        public static List<Result> LoadPage()
        {
            if (HttpContext.Current.Session["Results"] != null)
            {
                return (List<Result>)HttpContext.Current.Session["Results"];
            }

            string url = "https://www.soccerstats.com/latest.asp?league=england";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var document = doc.QuerySelectorAll("a[title*='stats']");
            var matches = document.QuerySelectorAll("a[href^='team.asp']");

            List<Result> results = new List<Result>();

            foreach (var match in matches)
            {
                var clubPage = web.Load($"https://www.soccerstats.com/{match.Attributes["href"].Value}");
                var h2s = clubPage.QuerySelectorAll("h2");
                var resultsElement = h2s.Where(t => t.InnerText.Contains("Results")).FirstOrDefault();
                var resultsTable = resultsElement.ParentNode.ParentNode.ParentNode.Descendants("tr").Where(t => t.InnerHtml.Contains(match.InnerText));

                foreach (var resultElement in resultsTable)
                {
                    Result result = new Result();

                    if (!resultElement.InnerHtml.Contains("Filter"))
                    {
                        string date = resultElement.SelectSingleNode("td[1]").InnerText.Replace("\n", "");
                        DateTime.TryParse(date, out DateTime matchDate);
                        if (matchDate.Month > 6)
                        {
                            matchDate = matchDate.AddYears(-1);
                        }
                        result.HomeTeam = resultElement.SelectSingleNode("td[2]").InnerText.Replace("\n", "");
                        result.AwayTeam = resultElement.SelectSingleNode("td[4]").InnerText.Replace("\n", "");
                        result.MatchDate = matchDate;

                        HtmlNode goalsNode = resultElement.SelectSingleNode("td[3]/a/font/b");
                        if (goalsNode != null)
                        {
                            if (int.TryParse(goalsNode.InnerText.ToString().Substring(0, 1), out int homeGoals))
                            {
                                result.HomeGoals = homeGoals;
                            }
                            if (int.TryParse(goalsNode.InnerText.ToString().Substring(4, 1), out int awayGoals))
                            {
                                result.AwayGoals = awayGoals;
                            }
                        }

                        if (!results.Any(r => r.MatchDate == matchDate && (r.HomeTeam == result.HomeTeam || r.HomeTeam == result.AwayTeam)))
                        {
                            results.Add(result);
                        }
                    }
                }
            }

            HttpContext.Current.Session["Results"] = results;
            return results;
        }
    }
}