// var dataSource
window.onload = function () {
    fetch('/api/SalesApi/GetSalesMonth', {
        method: 'Get',
        headers: {
            'content-Type': 'application/json;charset=utf-8,'
        }
    })
        .then((res) => {
            if (res.ok) {
                console.log('OK')
                // dataSource = res.json();
                Promise.resolve(res.json()).then((result) => {
                    let dataSource = [];
                    dataSource.length = 12;
                    dataSource.fill(0);
                    //法一:
                    // Object.keys(result).forEach((key,i) => {
                    //     dataSource[ result[key].Month-1 ] = result[key].SalesAmount
                    // });

                    //法二:
                    // Object.values(result).forEach((x,i) => {
                    //     dataSource[ x.Month-1 ] = x.SalesAmount
                    // });

                    //法三:
                    for (let x of result) {
                        // console.log(x)
                        dataSource[x.Month - 1] = x.SalesAmount
                    }
                    console.log(dataSource)
                    let params = {
                        data1: dataSource,
                        // data2: [65880, 52988, 88088, 82188, 52688, 51588, 40848],
                    }

                    initChart(params)
                })
            }
            else {
                console.log('Fail')
            }
        })

    fetch('/api/SalesApi/GetSalesSevenDays', {
        method: 'Get',
        headers: {
            'content-Type': 'application/json;charset=utf-8,'
        }
    })
        .then(res => { if (res.ok) return res.json() })
        .then((resp) => {
            console.log('OK~')
            let dataSource2 = [];
            let dateTime = [];

            dataSource2 = resp.map(x => { return x.SalesAmount });
            dateTime = resp.map(x => { return x.DateTime });

            console.log(dataSource2)
            let params2 = {
                data: dataSource2,
                data2: dateTime
            }

            barSevenChart(params2)
        })
        .catch(res => {
            console.log('Fail~')
        })
}

//折線圖
function initChart(params) {
    var ctx = document.getElementById("lineChart");
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            datasets: [
                {
                    label: '月銷售額',
                    data: params.data1,
                    fill: false,
                    borderColor: 'rgb(75, 192, 192)',
                    tension: 0.1
                },
                // {
                //     label: '第二條',
                //     data: params.data2,
                //     fill: false,
                //     borderColor: 'rgb(175, 192, 77)',
                //     tension: 0.1
                // },
            ]
        },
        options: {}
    });
}

//長條圖
function barSevenChart(params2) {
    var bar = document.getElementById("barChart");
    var myBarChart = new Chart(bar, {
        type: 'bar',
        data: {
            labels: params2.data2,
            datasets: [{
                label: '過去七天的營業額',
                data: params2.data,
                backgroundColor:
                    [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(255, 205, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(201, 203, 207, 0.2)'
                    ],
                borderColor:
                    [
                        'rgb(255, 99, 132)',
                        'rgb(255, 159, 64)',
                        'rgb(255, 205, 86)',
                        'rgb(75, 192, 192)',
                        'rgb(54, 162, 235)',
                        'rgb(153, 102, 255)',
                        'rgb(201, 203, 207)'
                    ],
                borderWidth: 1
            }]
        },
        options: {}
    });
}
