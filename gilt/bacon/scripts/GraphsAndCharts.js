var GILTY = GILTY || {};
GILTY.D3 =
{
    PieChart : function d3PieChart(Data, TargetId, WidthPx, HeightPx, RadiusPx)
    {
        $("#" + TargetId).empty();

        var color = d3.scale.category20c();     //builtin range of colors

        var vis = d3.select("#" + TargetId)
            .append("svg:svg")              //create the SVG element inside the <body>
            .data([Data])                   //associate our data with the document
                .attr("width", WidthPx)           //set the width and height of our visualization (these will be attributes of the <svg> tag
                .attr("height", HeightPx)
            .append("svg:g")                //make a group to hold our pie chart
                .attr("transform", "translate(" + RadiusPx + "," + RadiusPx + ")")    //move the center of the pie chart from 0, 0 to radius, radius

        var arc = d3.svg.arc()              //this will create <path> elements for us using arc data
            .outerRadius(RadiusPx);

        var pie = d3.layout.pie()                        //this will create arc data for us given a list of values
            .value(function (d) { return d.value; });    //we must tell it out to access the value of each element in our data array

        var arcs = vis.selectAll("g.slice")     //this selects all <g> elements with class slice (there aren't any yet)
            .data(pie)                          //associate the generated pie data (an array of arcs, each having startAngle, endAngle and value properties) 
            .enter()                            //this will create <g> elements for every "extra" data element that should be associated with a selection. The result is creating a <g> for every object in the data array
                .append("svg:g")                //create a group to hold each slice (we will have a <path> and a <text> element associated with each slice)
                    .attr("class", "slice");    //allow us to style things in the slices (like text)

        arcs.append("svg:path")
                .attr("fill", function (d, i) { return color(i); })
                .attr("d", arc);

        // add legend   
        var legend = d3.select("#" + TargetId).append("svg")
          .attr("class", "legend")
          .attr("width", WidthPx)
          .attr("height", HeightPx)
          .selectAll("g")
          .data(Data)
          .enter().append("g")
          .attr("transform", function (d, i) { return "translate(0," + i * 20 + ")"; });

        legend.append("rect")
          .attr("width", 18)
          .attr("height", 18)
          .style("fill", function (d, i) { return color(i); });

        legend.append("text")
          .attr("x", 24)
          .attr("y", 9)
          .attr("dy", ".35em")
          .text(function (d) { return d.label; });
    },
    ChoroPlethFillKey : function (Val, MaxVal)
    {
        var percent = (Val / MaxVal);    
        if (percent < 0.33)
        {
            return "LOW";
        }
        else if (( percent >= 0.33) && (percent < 0.66))
        {
            return "MEDIUM";
        }
        else if ((percent >= 0.66) && (percent < 1))
        {
            return "HIGH";
        }
        else 
        {
            return "NONE";
        }
    },
    ChoroPleth : function(Data, TargetId)
    {
        $("#" + TargetId).empty();

        //http://colorbrewer2.org/
        var map = new Datamap({
            element: document.getElementById(TargetId),
            fills: {
                LOW: '#ECE7F2',
                MEDIUM: '#A6BDDB',
                HIGH: '#2B8CBE',
                NONE: '#000000',
                defaultFill: '#000000'
            },
            data: Data,
            geographyConfig: {
                popupTemplate: function (geo, data)
                {
                    return ['<div class="hoverinfo"><strong>',
                            'Number of events in ' + geo.properties.name,
                            ': ' + (data ? data.numberOfThings : '0'),
                            '</strong></div>'].join('');
                }
            }
        });
        //draw a legend for this map
        map.legend();
    },
    ArcMap : function (Data, TargetId)
    {
        $("#" + TargetId).empty();
        var ipArc = new Datamap({
            scope: 'world',
            element: document.getElementById('WorldMapSvg'),
            projection: 'equirectangular'
        });
        ipArc.arc(Data, { strokeWidth: 2 });
    }
}