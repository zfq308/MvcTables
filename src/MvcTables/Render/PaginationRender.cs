﻿namespace MvcTables.Render
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Configuration;

    #endregion

    internal class HtmlPaginationRender
    {
        private readonly IPaginator _paginator;

        public HtmlPaginationRender(IPaginator paginator)
        {
            _paginator = paginator;
        }

        public void RenderPagination(PagingControlConfiguration pageConfig, string tableId, ControllerContext context)
        {
            var writer = WriterHelper.GetWriter(context);
            using (
                new ComplexContentTag("div",
                                      new Dictionary<String, object>
                                          {
                                              {
                                                  "class",
                                                  pageConfig.ContainerCssClass +
                                                  " mvctable-paginator"
                                              },
                                              {"data-target", tableId}
                                          }, writer))
            {
                using (new ComplexContentTag("ul", writer))
                {
                    using (new ComplexContentTag("li", writer))
                    {
                        using (
                            new ComplexContentTag("a",
                                                  new RouteValueDictionary(new { href = _paginator.First.Url }).WithClass(pageConfig.TableDefinition.FilterExpression),
                                                  writer))
                        {
                            writer.Write(pageConfig.FirstPageText);
                        }
                    }
                    using (new ComplexContentTag("li", writer))
                    {
                        using (
                            new ComplexContentTag("a",
                                                  new RouteValueDictionary(new {href = _paginator.Previous.Url})
                                                      .WithClass(pageConfig.TableDefinition.FilterExpression), writer))
                        {
                            writer.Write(pageConfig.PreviousPageText);
                        }
                    }
                    foreach (var page in _paginator.Pages)
                    {
                        object classObj = page.IsCurrent ? new {@class = pageConfig.ActiveCssClass} : null;

                        using (new ComplexContentTag("li", classObj, writer))
                        {
                            using (
                                new ComplexContentTag("a",
                                                      new RouteValueDictionary(new { href = page.Url }).WithClass(pageConfig.TableDefinition.FilterExpression),
                                                      writer))
                            {
                                writer.Write(page.PageNumber.ToString(Thread.CurrentThread.CurrentUICulture));
                            }
                        }
                    }
                    using (new ComplexContentTag("li", writer))
                    {
                        using (
                            new ComplexContentTag("a",
                                                  new RouteValueDictionary(new { href = _paginator.Next.Url }).WithClass(pageConfig.TableDefinition.FilterExpression),
                                                  writer))
                        {
                            writer.Write(pageConfig.NextPageText);
                        }
                    }
                    using (new ComplexContentTag("li", writer))
                    {
                        using (
                            new ComplexContentTag("a",
                                                  new RouteValueDictionary(new { href = _paginator.Last.Url }).WithClass(pageConfig.TableDefinition.FilterExpression),
                                                  writer))
                        {
                            writer.Write(pageConfig.LastPageText);
                        }
                    }
                }
            }
        }
    }
}