
var All_search =  window.location.search;
    //Url 解碼 , 當輸入的東西有中文時候 , window.location.href 會自動將中文字 進行編碼 , 所以需要解碼
All_search = decodeURI(All_search)
if (All_search  != '')
    {
        All_search = All_search.substring(1, All_search.length);
}

function UrlSearch(input,inputnum)
{
         let queryString=["City"]
         let queryName= queryString[inputnum]
         if(All_search=='')
         {
             window.location.search = `${queryName}=` + input;
             console.log('成功')
          }
         else if(!All_search.includes(`${queryName}`))
         {
             window.location.search = `${queryName}=` + input + "&" + All_search;
             console.log('成功')
         }
         else if(All_search.includes(`${queryName}`)) //要是搜尋路徑存在時候
         {
            let params = new URLSearchParams(All_search);
            let get_Date = params.get(`${queryName}`); //取的 typeDate 的值
            All_search = All_search.replace(`${queryName}=${get_Date}`,`${queryName}=${input}`);
             window.location.search = All_search;
             console.log('成功')
          }
}
  
function DownUrl(inputnum)    //取消單個Url
{
    let queryName= queryString[inputnum];
    let params = new URLSearchParams(All_search);
    let get_Date = params.get(`${queryName}`);
    All_search = All_search.replace(`${queryName}=${get_Date}`,'');
    window.location.search = All_search ;
}

    const Today = new Date()
    let startDay = Today.getDate()<10 ?  ('0' + Today.getDate()) :  Today.getDate();
    let startMonth =  (Today.getMonth() +1)<10 ?('0' + (Today.getMonth() +1)) :  (Today.getMonth() +1);
    let startYear =  Today.getFullYear();

    let startDate = startMonth + '/' + startDay + '/' + startYear;
    let endDate = startMonth + '/' + startDay + '/' + startYear;

    if(All_search.includes("typeDate"))
    {
        let params = new URLSearchParams(All_search);
        let get_Date = params.get("typeDate"); //取的 typeDate 的值
        let finalDate = get_Date.split(',')
        startDate = finalDate[0];
        endDate =finalDate[1];
    }
    $(function()
    {
        $('input[name="datefilter"]').daterangepicker({
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            },
            minDate: moment(),
            startDate: startDate,
            endDate: endDate
        });

    $('input[name="datefilter"]').on('apply.daterangepicker', function(ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
            });

    $('input[name="datefilter"]').on('cancel.daterangepicker', function(ev, picker) {
        $(this).val(' ');
            });

            //$(".drp-buttons>.applyBtn").on('click', () => {PostDate}); 
        document.querySelectorAll(".drp-buttons>.applyBtn").forEach((Applybtn, i) => {
            Applybtn.addEventListener('click', () => {
                classifyCardVue.filterPost(1);

                //var drap_select = document.querySelectorAll("span.drp-selected");
                //var dateRange = drap_select[i].innerText.split(' - ');
                //PostDate(dateRange);

                //UrlSearch(splitDate, 0);
            })
        });
        document.querySelectorAll('.cancelBtn').forEach(item => {
            item.addEventListener('click', () => {
                item.value = "篩選出發日期";
                startDate = startMonth + '/' + startDay + '/' + startYear;
                endDate = startMonth + '/' + startDay + '/' + startYear;
                /*                   DownUrl(0);*/
            })
        })
        });
    function PostDate(dateRange)
    {
        let dateRangeList = []
        dateRange.forEach(d =>{
            let data =  d.substring(6,10) + '-' +  d.substring(0,2) + '-' +d.substring(3, 5)
            console.log(data)
            dateRangeList.push(data)
        })

        fetch("/api/apiDate/Date", {
            method: 'POST',
            headers: {'Content-Type': 'application/json;charset=utf-8' },
            //body 轉json
            body: JSON.stringify({
                dateRange : dateRange,
            })
        })
        .then((response) => {
            if (response.status == 200) {
                console.log("成功");
                return response.json();
                }
            })

        .catch((error) => {
            console.log("失敗")
        })

    }

