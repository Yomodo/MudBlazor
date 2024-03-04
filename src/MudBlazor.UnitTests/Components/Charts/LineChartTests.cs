﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MudBlazor.Charts;
using MudBlazor.UnitTests.Components;
using NUnit.Framework;
using Bunit;
using AngleSharp.Dom;

namespace MudBlazor.UnitTests.Charts
{
    public class LineChartTests: BunitTest
    {
        private readonly string[] _baseChartPalette = 
        {
            "#2979FF", "#1DE9B6", "#FFC400", "#FF9100", "#651FFF", "#00E676", "#00B0FF", "#26A69A", "#FFCA28",
            "#FFA726", "#EF5350", "#EF5350", "#7E57C2", "#66BB6A", "#29B6F6", "#FFA000", "#F57C00", "#D32F2F",
            "#512DA8", "#616161"
        };
        
        private readonly string[] _modifiedPalette =
        {
            "#264653", "#2a9d8f", "#e9c46a", "#f4a261", "#e76f51"
        };

        private readonly string[] _customPalette =
        {
            "#015482", "#CC1512", "#FFE135", "#087830", "#D70040", "#B20931", "#202E54", "#F535AA", "#017B92",
            "#FA4224", "#062A78", "#56B4BE", "#207000", "#FF43A4", "#FB8989", "#5E9B8A", "#FFB7CE", "#C02B18",
            "#01153E", "#2EE8BB", "#EBDDE2"
        };

        private static Array GetInterpolationOptions()
        {
            return Enum.GetValues(typeof(InterpolationOption));
        }
        
        [SetUp]
        public void Init()
        {
 
        }

        [Test]
        public void LineChartEmptyData()
        {
            var comp = Context.RenderComponent<Bar>();
            comp.Markup.Should().Contain("mud-chart");
        }

        [Theory]
        [TestCaseSource("GetInterpolationOptions")]
        public void LineChartExampleData(InterpolationOption opt)
        {
            List<ChartSeries> chartSeries = new List<ChartSeries>()
            {
                new ChartSeries() { Name = "Series 1", Data = new double[] { 90, 79, -72, 69, 62, 62, -55, 65, 70 } },
                new ChartSeries() { Name = "Series 2", Data = new double[] { 10, 41, 35, 51, 49, 62, -69, 91, -148 } },
                new ChartSeries() { Name = "Series 3", Data = new double[] { 10, 41, 35, 51, 49, 62, -69, 91, -148 }, IsVisible = false }
            };
            string[] xAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };
            
            var comp = Context.RenderComponent<MudChart>(parameters => parameters
                .Add(p => p.ChartType, ChartType.Line)
                .Add(p => p.Height, "350px")
                .Add(p => p.Width, "100%")
                .Add(p => p.ChartSeries, chartSeries)
                .Add(p => p.XAxisLabels, xAxisLabels)
                .Add(p => p.ChartOptions, new ChartOptions { ChartPalette = _baseChartPalette, InterpolationOption = opt}));

            comp.Instance.ChartSeries.Should().NotBeEmpty();
            
            comp.Markup.Should().Contain("class=\"mud-charts-xaxis\"");
            comp.Markup.Should().Contain("class=\"mud-charts-yaxis\"");
            comp.Markup.Should().Contain("mud-chart-legend-item");
            
            if (chartSeries.Count <= 3)
            {
                comp.Markup.Should().
                    Contain("Series 1").And.Contain("Series 2");
            }

            if (chartSeries.FirstOrDefault(x => x.Name == "Series 1") is not null)
            {
                switch (opt)
                {
                    case InterpolationOption.NaturalSpline:
                        comp.Markup.Should().Contain("d=\"M 30 36.53846153846155 L 37.28395061728395 30.76876791378725 L 44.5679012345679 25.425675767531445 L 51.851851851851855 20.935786578112616 L 59.135802469135804 17.72570182394925 L 66.41975308641975 16.22202298345984 L 73.70370370370371 16.851351535062875 L 80.98765432098766 20.04028895717684 L 88.27160493827161 26.215436728220233 L 95.55555555555556 35.80339632661153 L 102.8395061728395 49.230769230769226 L 110.12345679012346 66.65909390223177 L 117.40740740740742 87.18965673501755 L 124.69135802469137 109.65868110626485 L 131.97530864197532 132.90239039311203 L 139.25925925925927 155.75700797269738 L 146.54320987654322 177.05875722215927 L 153.82716049382717 195.643861518636 L 161.11111111111111 210.34854423926583 L 168.39506172839506 220.00902876118724 L 175.679012345679 223.46153846153845 L 182.96296296296296 219.94254878497787 L 190.2469135802469 210.2895434462445 L 197.53086419753086 195.74025822759714 L 204.81481481481484 177.5324289112949 L 212.0987654320988 156.9037912795967 L 219.38271604938274 135.09208111476153 L 226.66666666666669 113.3350341990484 L 233.95061728395063 92.87038631471621 L 241.23456790123458 74.93587324402404 L 248.51851851851853 60.769230769230774 L 255.80246913580248 51.27840326554889 L 263.08641975308643 46.052169480004544 L 270.3703703703704 44.34951675257734 L 277.65432098765433 45.42943242324688 L 284.9382716049383 48.55090383199276 L 292.22222222222223 52.972918318794626 L 299.5061728395062 57.954463223632075 L 306.7901234567901 62.75452588648469 L 314.0740740740741 66.63209364733204 L 321.358024691358 68.84615384615387 L 328.641975308642 68.88806892205736 L 335.9259259259259 67.17870171066049 L 343.2098765432099 64.37129014670899 L 350.4938271604938 61.11907216494847 L 357.77777777777777 58.075285700124645 L 365.0617283950617 55.893168686983145 L 372.34567901234567 55.22595906026965 L 379.6296296296297 56.72689475472983 L 386.9135802469136 61.04921370510935 L 394.1975308641976 68.84615384615387 L 401.4814814814815 80.493167200068 L 408.7654320987655 95.25456213889206 L 416.0493827160494 112.11686112212533 L 423.33333333333337 130.06658660926703 L 430.6172839506173 148.0902610598165 L 437.90123456790127 165.17440693327293 L 445.1851851851852 180.30554668913564 L 452.46913580246917 192.47020278690383 L 459.7530864197531 200.65489768607682 L 467.03703703703707 203.84615384615387 L 474.320987654321 201.35772381613234 L 481.60493827160496 193.81228050300217 L 488.8888888888889 182.15972690325142 L 496.17283950617286 167.34996601336812 L 503.4567901234568 150.3329008298403 L 510.74074074074076 132.05843434915602 L 518.0246913580247 113.47646956780338 L 525.3086419753087 95.53690948227035 L 532.5925925925926 79.18965708904501 L 539.8765432098766 65.38461538461542 L 547.1604938271605 54.85516830463354 L 554.4444444444445 47.468623541407084 L 561.7283950617284 42.87576972640765 L 569.0123456790124 40.72739549110687 L 576.2962962962963 40.67428946697636 L 583.5802469135803 42.367240285487746 L 590.8641975308642 45.45703657811266 L 598.1481481481482 49.59446697632268 L 605.4320987654321 54.430320111589495 L 612.716049382716 59.61538461538464\"");
                        break;
                    case InterpolationOption.Straight:
                        comp.Markup.Should()
                            .Contain("d=\"M 30 36.53846153846155 L 103.75 49.230769230769226 L 177.5 223.46153846153845 L 251.25 60.769230769230774 L 325 68.84615384615387 L 398.75 68.84615384615387 L 472.5 203.84615384615387 L 546.25 65.38461538461542 L 620 59.61538461538464\"");
                        break;
                    case InterpolationOption.EndSlope:
                        comp.Markup.Should().Contain("d=\"M 30 36.53846153846155 L 37.28395061728395 35.640620751671015 L 44.5679012345679 33.40254899739436 L 51.851851851851855 30.507422184773997 L 59.135802469135804 27.63841622295231 L 66.41975308641975 25.478707021071713 L 73.70370370370371 24.71147048827461 L 80.98765432098766 26.01988253370341 L 88.27160493827161 30.087119066500513 L 95.55555555555556 37.5963559958083 L 102.8395061728395 49.230769230769226 L 110.12345679012346 65.35423792058457 L 117.40740740740742 85.05345417469127 L 124.69135802469137 107.09581334258523 L 131.97530864197532 130.2487107737623 L 139.25925925925927 153.27954181771833 L 146.54320987654322 174.9557018239492 L 153.82716049382717 194.04458614195082 L 161.11111111111111 209.31359012121897 L 168.39506172839506 219.53010911124954 L 175.679012345679 223.46153846153845 L 182.96296296296296 220.29011987368298 L 190.2469135802469 210.85748045768665 L 197.53086419753086 196.42009367565421 L 204.81481481481484 178.2344329896907 L 212.0987654320988 157.556971861901 L 219.38271604938274 135.64418375438996 L 226.66666666666669 113.75254212926248 L 233.95061728395063 93.13852044862355 L 241.23456790123458 75.05859217457797 L 248.51851851851853 60.769230769230774 L 255.80246913580248 51.192974892375666 L 263.08641975308643 45.91662399456215 L 270.3703703703704 44.193042724028565 L 277.65432098765433 45.27509572901328 L 284.9382716049383 48.41564765775463 L 292.22222222222223 52.86756315849102 L 299.5061728395062 57.883706879460775 L 306.7901234567901 62.716943468902286 L 314.0740740740741 66.62013757505385 L 321.358024691358 68.84615384615387 L 328.641975308642 68.88221132604511 L 335.9259259259259 67.1529466409879 L 343.2098765432099 64.31735081284697 L 350.4938271604938 61.03441486348706 L 357.77777777777777 57.963129814772884 L 365.0617283950617 55.76248668856919 L 372.34567901234567 55.09147650674071 L 379.6296296296297 56.60909029115217 L 386.9135802469136 60.97431906366832 L 394.1975308641976 68.84615384615387 L 401.4814814814815 80.60202595729017 L 408.7654320987655 95.49312790302483 L 416.0493827160494 112.48909248612215 L 423.33333333333337 130.55955250934633 L 430.6172839506173 148.6741407754617 L 437.90123456790127 165.80249008723237 L 445.1851851851852 180.91423324742266 L 452.46913580246917 192.97900305879693 L 459.7530864197531 200.96643232411918 L 467.03703703703707 203.84615384615387 L 474.320987654321 200.92814638325595 L 481.60493827160496 192.88377251614367 L 488.8888888888889 180.72474078112612 L 496.17283950617286 165.46275971451234 L 503.4567901234568 148.1095378526113 L 510.74074074074076 129.67678373173223 L 518.0246913580247 111.176205888184 L 525.3086419753087 93.61951285827577 L 532.5925925925926 78.01841317831658 L 539.8765432098766 65.38461538461542 L 547.1604938271605 56.464619278917 L 554.4444444444445 50.94408972470831 L 561.7283950617284 48.24348285091201 L 569.0123456790124 47.7832547864507 L 576.2962962962963 48.983861660247 L 583.5802469135803 51.26575960122355 L 590.8641975308642 54.049404738302975 L 598.1481481481482 56.75525320040788 L 605.4320987654321 58.803761116460876 L 612.716049382716 59.61538461538464\"");
                    break;
                    case InterpolationOption.Periodic:
                        comp.Markup.Should().Contain("d=\"M 30 36.53846153846155 L 37.28395061728395 36.35384615384617 L 44.5679012345679 34.570329670329684 L 51.851851851851855 31.90865384615386 L 59.135802469135804 29.08956043956045 L 66.41975308641975 26.83379120879122 L 73.70370370370371 25.86208791208791 L 80.98765432098766 26.89519230769232 L 88.27160493827161 30.653846153846146 L 95.55555555555556 37.85879120879122 L 102.8395061728395 49.230769230769226 L 110.12345679012346 65.16328296703297 L 117.40740740740742 84.7408791208791 L 124.69135802469137 106.72086538461537 L 131.97530864197532 129.8605494505494 L 139.25925925925927 152.917239010989 L 146.54320987654322 174.64824175824174 L 153.82716049382717 193.81086538461534 L 161.11111111111111 209.16241758241756 L 168.39506172839506 219.46020604395605 L 175.679012345679 223.46153846153845 L 182.96296296296296 220.34071428571428 L 190.2469135802469 210.93999999999997 L 197.53086419753086 196.51865384615382 L 204.81481481481484 178.33593406593408 L 212.0987654320988 157.65109890109892 L 219.38271604938274 135.7234065934066 L 226.66666666666669 113.81211538461537 L 233.95061728395063 93.17648351648351 L 241.23456790123458 75.07576923076925 L 248.51851851851853 60.769230769230774 L 255.80246913580248 51.18155219780221 L 263.08641975308643 45.8991208791209 L 270.3703703703704 44.17375000000002 L 277.65432098765433 45.257252747252764 L 284.9382716049383 48.40144230769234 L 292.22222222222223 52.85813186813189 L 299.5061728395062 57.87913461538466 L 306.7901234567901 62.71626373626376 L 314.0740740740741 66.62133241758245 L 321.358024691358 68.84615384615387 L 328.641975308642 68.87730769230772 L 335.9259259259259 67.14043956043959 L 343.2098765432099 64.29596153846155 L 350.4938271604938 61.00428571428574 L 357.77777777777777 57.925824175824204 L 365.0617283950617 55.720989010989044 L 372.34567901234567 55.05019230769234 L 379.6296296296297 56.57384615384616 L 386.9135802469136 60.952362637362654 L 394.1975308641976 68.84615384615387 L 401.4814814814815 80.63306318681322 L 408.7654320987655 95.56065934065938 L 416.0493827160494 112.59394230769233 L 423.33333333333337 130.69791208791213 L 430.6172839506173 148.83756868131871 L 437.90123456790127 165.9779120879121 L 445.1851851851852 181.08394230769233 L 452.46913580246917 193.12065934065942 L 459.7530864197531 201.05306318681318 L 467.03703703703707 203.84615384615387 L 474.320987654321 200.80890109890112 L 481.60493827160496 192.6261538461539 L 488.8888888888889 180.32673076923078 L 496.17283950617286 164.93945054945056 L 503.4567901234568 147.49313186813188 L 510.74074074074076 129.01659340659344 L 518.0246913580247 110.5386538461539 L 525.3086419753087 93.08813186813191 L 532.5925925925926 77.69384615384615 L 539.8765432098766 65.38461538461542 L 547.1604938271605 56.91056318681322 L 554.4444444444445 51.907032967033004 L 561.7283950617284 49.73067307692311 L 569.0123456790124 49.73813186813191 L 576.2962962962963 51.28605769230772 L 583.5802469135803 53.731098901098946 L 590.8641975308642 56.429903846153884 L 598.1481481481482 58.73912087912093 L 605.4320987654321 60.015398351648386 L 612.716049382716 59.61538461538464\"");
                        break;
                }
            }

            if (comp.Instance.ChartOptions.InterpolationOption == InterpolationOption.Straight && chartSeries.FirstOrDefault(x => x.Name == "Series 2") is not null)
            {
                comp.Markup.Should()
                    .Contain("d=\"M 30 128.84615384615384 L 103.75 93.07692307692307 L 177.5 100 L 251.25 81.53846153846152 L 325 83.84615384615387 L 398.75 68.84615384615387 L 472.5 220 L 546.25 35.38461538461536 L 620 311.1538461538462\"");
            }
            
            comp.SetParametersAndRender(parameters => parameters
                .Add(p => p.ChartOptions, new ChartOptions(){ChartPalette = _modifiedPalette}));

            comp.Markup.Should().Contain(_modifiedPalette[0]);

            comp.Markup.Should().Contain("class=\"mud-charts-xaxis\"");
            comp.Markup.Should().Contain("class=\"mud-charts-yaxis\"");
            comp.Markup.Should().Contain("mud-chart-legend-item");

            comp.SetParametersAndRender(parameters => parameters
                .Add(p => p.CanHideSeries, true)
                .Add(p => p.ChartOptions, new ChartOptions() { ChartPalette = _baseChartPalette, InterpolationOption = opt }));

            if (comp.Instance.CanHideSeries)
            {
                var seriesCheckboxes = comp.FindAll(".mud-checkbox-input");

                comp.InvokeAsync(() => {
                    seriesCheckboxes[0].Change(false); 
                });

                seriesCheckboxes = comp.FindAll(".mud-checkbox-input");

                comp.InvokeAsync(() => {
                    seriesCheckboxes[2].Change(true);  
                });

                seriesCheckboxes = comp.FindAll(".mud-checkbox-input");
                
                seriesCheckboxes[0].IsChecked().Should().BeFalse();
                seriesCheckboxes[1].IsChecked().Should().BeTrue();
                seriesCheckboxes[2].IsChecked().Should().BeTrue();
            }
        }

        [Test]
        public void LineChartColoring()
        {
            List<ChartSeries> chartSeries = new List<ChartSeries>()
            {
                new ChartSeries() { Name = "Deep Sea Blue", Data = new double[] { 40, 20, 25, 27, 46 } },
                new ChartSeries() { Name = "Venetian Red", Data = new double[] { 19, 24, 35, 13, 28 } },
                new ChartSeries() { Name = "Banana Yellow", Data = new double[] { 8, 6, 11, 13, 4 } },
                new ChartSeries() { Name = "La Salle Green", Data = new double[] { 18, 9, 7, 10, 7 } },
                new ChartSeries() { Name = "Rich Carmine", Data = new double[] { 9, 14, 6, 15, 20 } },
                new ChartSeries() { Name = "Shiraz", Data = new double[] { 9, 4, 11, 5, 19 } },
                new ChartSeries() { Name = "Cloud Burst", Data = new double[] { 14, 9, 20, 16, 6 } },
                new ChartSeries() { Name = "Neon Pink", Data = new double[] { 14, 8, 4, 14, 8 } },
                new ChartSeries() { Name = "Ocean", Data = new double[] { 11, 20, 13, 5, 5 } },
                new ChartSeries() { Name = "Orangey Red", Data = new double[] { 6, 6, 19, 20, 6 } },
                new ChartSeries() { Name = "Catalina Blue", Data = new double[] { 3, 2, 20, 3, 10 } },
                new ChartSeries() { Name = "Fountain Blue", Data = new double[] { 3, 18, 11, 12, 3 } },
                new ChartSeries() { Name = "Irish Green", Data = new double[] { 20, 5, 15, 16, 13 } },
                new ChartSeries() { Name = "Wild Strawberry", Data = new double[] { 15, 9, 12, 12, 1 } },
                new ChartSeries() { Name = "Geraldine", Data = new double[] { 5, 13, 19, 15, 8 } },
                new ChartSeries() { Name = "Grey Teal", Data = new double[] { 12, 16, 20, 16, 17 } },
                new ChartSeries() { Name = "Baby Pink", Data = new double[] { 1, 18, 10, 19, 8 } },
                new ChartSeries() { Name = "Thunderbird", Data = new double[] { 15, 16, 10, 8, 5 } },
                new ChartSeries() { Name = "Navy", Data = new double[] { 16, 2, 3, 5, 5 } },
                new ChartSeries() { Name = "Aqua Marina", Data = new double[] { 17, 6, 11, 19, 6 } },
                new ChartSeries() { Name = "Lavender Pinocchio", Data = new double[] { 1, 11, 4, 18, 1 } },
                new ChartSeries() { Name = "Deep Sea Blue", Data = new double[] { 1, 11, 4, 18, 1 } }
            };

            var comp = Context.RenderComponent<MudChart>(parameters => parameters
                .Add(p => p.ChartType, ChartType.Line)
                .Add(p => p.Height, "350px")
                .Add(p => p.Width, "100%")
                .Add(p => p.ChartOptions, new ChartOptions { ChartPalette = new string[] { "#1E9AB0" } })
                .Add(p => p.ChartSeries, chartSeries));

            var paths1 = comp.FindAll("path");

            int count;
            count = paths1.Count(p => p.OuterHtml.Contains($"stroke=\"{"#1E9AB0"}\""));
            count.Should().Be(22);

            comp.SetParametersAndRender(parameters => parameters
                .Add(p => p.ChartOptions, new ChartOptions() { ChartPalette = _customPalette }));

            var paths2 = comp.FindAll("path");

            foreach (var color in _customPalette)
            {
                count = paths2.Count(p => p.OuterHtml.Contains($"stroke=\"{color}\""));
                if (color == _customPalette[0])
                {
                    count.Should().Be(2, because: "the number of series defined exceeds the number of colors in the chart palette, thus, any new defined series takes the color from the chart palette in the same fashion as the previous series starting from the beginning");
                }
                else
                {
                    count.Should().Be(1);
                }
            }
        }
    }
}
