<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/DefaultLoggedIn.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppActs.Client.WebSite.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Appacts | Dashboard</title>

    <script language="javascript" type="text/javascript" src="/JavaScript/Library/datepicker-eye.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/Library/datepicker-utils.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/Library/datepicker.js"></script>
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="/JavaScript/Library/excanvas.min.js"></script><![endif]-->
    <script language="javascript" type="text/javascript" src="/JavaScript/Library/jquery.flot.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/Library/jquery.flot.pie.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/Library/jquery-ui-1.7.3.custom.min.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/Library/mustache.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/Extensions/dateRange.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/appacts.tile.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/appacts.report.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/appacts.report.chart.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/appacts.report.data.compare.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/appacts.report.data.normal.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/appacts.report.data.detail.js"></script>
    <script language="javascript" type="text/javascript" src="/Default.effects.js"></script>
    <script language="javascript" type="text/javascript" src="/JavaScript/Library/jquery.nicescroll.js"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">

<script type="text/javascript">
    var applicationId = '<%= this.application.Guid %>';
</script>

<div id="loadingOverlay">
    <div id="overlay" style="display:block;"></div>
    <div id="loading"></div>
</div>

<div id="report">
    <div class="borderLeftMed">
        <div class="borderRightMed">

            <div id="dataDimensions">

                <div id="optionsViewSwitchAndParams">
                    <div id="options">
                        <span id="normal" class="select single">Normal</span>
                        <span id="compareApplications">Compare Applications</span>
                        <span id="comparePlatforms">Compare Platforms</span>
                        <span id="compareVersions">Compare Versions</span>
                    </div>

                    <div id="viewSwitchAndParams">
                        <div id="views">
                            <span class="title">Change view:</span>
                            <div id="chart" class="switchOn">CHART</div>
                            <div id="table" class="switchOff">TABLE</div>                   
                        </div>
                        <div id="params">
                        </div>                    
                    </div>
                </div>

                <div id="appOptions">
                    <div class="control">
                        <span id="app">Select app:</span>
                        <asp:DropDownList runat="server" ID="ddlApplications" DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                        <script type="text/javascript" language="javascript">
                            var ddlApplications = '#<%= ddlApplications.ClientID %>';
                            $(document).ready(function () {
                                $('#<%= ddlApplications.ClientID %>').msDropDown({ showIcon: false });
                            });
                        </script>
                    </div>
                    <div class="control">
                        <span id="date">Change time:</span>
                        <asp:DropDownList runat="server" ID="ddlDate">
                            <asp:ListItem Text="Today" Value="dateChangedToday" />
                            <asp:ListItem Text="Yesterday" Value="dateChangedYesterday"  />
                            <asp:ListItem Text="1 Week" Value="dateChanged1Week" />
                            <asp:ListItem Text="2 Weeks" Value="dateChanged2Weeks" />
                            <asp:ListItem Text="1 Month" Value="dateChanged1Month" />
                            <asp:ListItem Text="2 Months" Value="dateChanged2Months" />
                            <asp:ListItem Text="3 Months" Value="dateChanged3Months" />
                            <asp:ListItem Text="6 Months" Value="dateChanged6Months" />
                            <asp:ListItem Text="1 Year" Value="dateChanged1Year" />
                            <asp:ListItem Text="Custom" Value="dateChange" />
                        </asp:DropDownList>
                        <script type="text/javascript" language="javascript">
                            var ddlDate = '#<%= ddlDate.ClientID %>';
                            $(document).ready(function () {
                                $('#<%= ddlDate.ClientID %>').msDropDown({ showIcon: false });
                            });
                        </script>                    
                    </div>
                    <div id="custom">
                        <span id="dateChange"></span>
                        <span id="dateRange"></span>

                        <div id="customDateRangeSelection" class="hide">
                            <div class="calendarsContainer">
                                <div class="calendarContainer">
                                    <span class="title">Select a start date:</span>
                                    <div id="customCalendarFrom"></div>                                
                                </div>
                                <div class="calendarContainer" style="float:right;">
                                    <span class="title">Select a end date:</span>
                                    <div id="customCalendarTo"></div>                                 
                                </div>
                            </div>
                            <input type="button" class="callToAction" value="Apply" id="dateChangeApply" />
                        </div>

                    </div>
                </div>


            </div>

            <div id="stage">

                <div id="name">
                    <span id="title"></span>
                    <span id="desc"></span>
                </div>

                <div id="NoData">No graph to show, click on the tile below.</div> 

                <div id="graph" class="hide" style="width:960px;height:308px;"></div>
             
                <div id="data" class="hide">
                    <div id="normal" class="hide">
                        <div id="table" class="normal"></div>
                        <script id="template" type="text/template">
                            <table>
                                <thead>
                                    <tr>
                                        <th class="title">
                                 
                                        </th>
                                        {{#axis}}
                                            <td class="x">{{X}}</td>
                                        {{/axis}}

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th class="title">
                                            {{yLabel}}
                                        </th>
                                        {{#axis}}
                                            <td class="y">{{Y}}</td>
                                        {{/axis}}
                                    </tr>
                                </tbody>
                            </table>
                        </script>
                        <div id="paging">
                            <div id="controls">
                                <span id="next">Next Page ></span>
                                <span id="prev">< Prev Page</span>
                            </div>
                            <div id="page">
                                Page <span id="current"></span> of <span id="total"></span>
                            </div>
                        </div>                
                    </div>

                    <div id="compareApplications" class="hide">
                        <div id="selectors"></div>   
                        <script id="templateSelectors" type="text/template">
                        <span id="message">Compare between 1 to 3 applications</span>
                        <ul>
                            {{#Options}}
                            <li> 
                                <input type="checkbox" value="{{Guid}}" />
                                <span>{{Name}}</span>
                            </li>
                            {{/Options}}
                        </ul>
                        </script>    
                        <div id="table" class="compare"></div>
                        <script id="templateTriple" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header2">
                                                <span>{{header1}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                            {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header3">
                                               <span> {{header2}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>
                        <script id="templateDouble" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header2">
                                                <span>{{header1}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                            {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>
                        <script id="templateSingle" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>
                        <div id="paging">
                            <div id="page">
                                Page <span id="current"></span> of <span id="total"></span>
                            </div>
                            <div id="controls">
                                <span id="next">Next Page ></span>
                                <span id="prev">< Prev Page</span>
                            </div>
                        </div>    
                    </div>

                    <div id="comparePlatforms" class="hide">
                        <div id="selectors"></div>  
                            <script id="templateSelectors" type="text/template">
                            <span id="message">Compare between 1 to 3 platforms</span>
                            <ul>
                            {{#Options}}
                            <li> 
                                <input type="checkbox" value="{{Id}}" />
                                <span>{{Name}}</span>
                            </li>
                            {{/Options}}
                        </ul>
                        </script>    
                        <div id="table" class="compare"></div>
                        <script id="templateTriple" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header2">
                                                <span>{{header1}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                            {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header3">
                                               <span> {{header2}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>   
                            <script id="templateDouble" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header2">
                                                <span>{{header1}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                            {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>   
                            <script id="templateSingle" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script> 
                        <div id="paging">
                            <div id="page">
                                Page <span id="current"></span> of <span id="total"></span>
                            </div>
                            <div id="controls">
                                <span id="next">Next Page ></span>
                                <span id="prev">< Prev Page</span>
                            </div>
                        </div>   
                    </div>

                    <div id="compareVersions" class="hide">
                        <div id="selectors"></div>   
                            <script id="templateSelectors" type="text/template">
                            <span id="message">Compare between 1 to 3 versions</span>
                        <ul>
                            {{#Options}}
                            <li> 
                                <input type="checkbox" value="{{.}}" />
                                <span>{{.}}</span>
                            </li>
                            {{/Options}}
                        </ul>
                        </script>     
                        <div id="table" class="compare"></div>
                        <script id="templateSingle" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>
                        <script id="templateDouble" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header2">
                                                <span>{{header1}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                            {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>
                        <script id="templateTriple" type="text/template">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="title">
                                            </th>
                                            {{#data}}
                                                <td class="x">
                                                    {{Column0}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th class="header1">
                                                <span>{{header0}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column1}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header2">
                                                <span>{{header1}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                            {{/data}}
                                        </tr>
                                        <tr>
                                            <th class="header3">
                                               <span> {{header2}}</span>
                                            </th>
                                            {{#data}}
                                                <td class="y">
                                                    {{Column2}}
                                                </td>
                                             {{/data}}
                                        </tr>
                                    </tbody>
                                </table>
                        </script>
                        <div id="paging">
                            <div id="page">
                                Page <span id="current"></span> of <span id="total"></span>
                            </div>
                            <div id="controls">
                                <span id="next">Next Page ></span>
                                <span id="prev">< Prev Page</span>
                            </div>
                        </div> 
                    </div>

                </div>

                <div id="detailReport" style="display:none;">
                    <div class="point"></div>
                    <div class="borderLeftMed">
                        <div class="borderRightMed">
                            <div class="detailReportTable">
                                <span id="loading">Loading...</span>
                            </div>
                        </div>
                    </div>
                </div>

            </div>  

        </div>
    </div>
       
</div>  

<div id="tileStage">
    <div class="borderLeftMed">
        <div class="borderRightMed">
            <div id="tiles"></div>
            <div id="noData">No tiles to show, select different data range? Not integrated yet, <asp:HyperLink runat="server" NavigateUrl="http://sourceforge.net/p/appacts">download our SDK now</asp:HyperLink>.</div> 
            
        </div>
    </div>
</div>

<div id="tileLibrary">
    <asp:Repeater runat="server" ID="rptTiles">
        <ItemTemplate>
            <div id='<%# ((string)Eval("Name")).Replace(" ", "") %>'>
                <input type="hidden" id="tileId" value='<%# Eval("Summary.Guid") %>' />
                <input type="hidden" id="reportId" value='<%# Eval("ReportNormal.Guid") %>' />

                <%# Eval("Summary.Template.Value") %>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<div id="detailLibrary" style="display:none;">
    <asp:Repeater runat="server" ID="rptDetails">
        <ItemTemplate>
            <div id='<%# Eval("Guid") %>'>
                <%# Eval("Template.Value") %>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

</asp:Content>
