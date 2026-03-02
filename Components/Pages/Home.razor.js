// Import Chart.js from local file (offline support)
const script = document.createElement('script');
script.src = '/chart.umd.js';
document.head.appendChild(script);

// Import Chart.js annotation plugin from local file (offline support)
const annotationScript = document.createElement('script');
annotationScript.src = '/chartjs-plugin-annotation.esm.js';
document.head.appendChild(annotationScript);

let chart1 = null;
let chart2 = null;

export function initializeCharts(canvas1, canvas2) {
    // Return a promise that resolves when charts are ready
    return new Promise((resolve) => {
        const checkChartReady = () => {
            if (typeof Chart !== 'undefined') {
                // Register the annotation plugin if available
                if (typeof ChartAnnotation !== 'undefined') {
                    Chart.register(ChartAnnotation);
                }
                createCharts(canvas1, canvas2);
                resolve();
            } else {
                // Chart not ready yet, check again in a moment
                setTimeout(checkChartReady, 50);
            }
        };
        checkChartReady();
    });
}

function createCharts(canvas1, canvas2) {
    // Determine if dark mode is active
    const isDarkMode = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    const textColor = isDarkMode ? '#e0e0e0' : '#000000';
    const gridColor = isDarkMode ? '#404040' : '#ddd';
    const lineColor = isDarkMode ? '#64b5f6' : 'rgb(75, 192, 192)';
    const lineColor2 = isDarkMode ? '#ef9a9a' : 'rgb(255, 99, 132)';
    
    // Chart 1: Diffraction Efficiency (Line Chart with empty datasets)
    const ctx1 = canvas1.getContext('2d');
    chart1 = new Chart(ctx1, {
        type: 'line',
        data: {
            labels: [],
            datasets: [
                {
                    label: 'S-Pol',
                    data: [],
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.1)',
                    tension: 0.1,
                    fill: false,
                    pointRadius: 0,
                    borderDash: [5, 5]
                },
                {
                    label: 'P-Pol',
                    data: [],
                    borderColor: 'rgb(54, 162, 235)',
                    backgroundColor: 'rgba(54, 162, 235, 0.1)',
                    tension: 0.1,
                    fill: false,
                    pointRadius: 0,
                    borderDash: [10, 5]
                },
                {
                    label: 'Average',
                    data: [],
                    borderColor: 'rgb(76, 175, 80)',
                    backgroundColor: 'rgba(76, 175, 80, 0.1)',
                    tension: 0.1,
                    fill: false,
                    pointRadius: 0,
                    borderDash: [2, 4]
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            animation: {
                duration: 300
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Efficiency Curves',
                    color: textColor
                },
                legend: {
                    position: 'bottom',
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    max: 1.0,
                    title: {
                        display: true,
                        text: 'Diffraction Efficiency',
                        color: textColor
                    },
                    ticks: {
                        color: textColor
                    },
                    grid: {
                        color: gridColor
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Wavelength (nm)',
                        color: textColor
                    },
                    ticks: {
                        color: textColor
                    },
                    grid: {
                        color: gridColor
                    }
                }
            }
        }
    });

    // Chart 2: Modulation Curves (Line Chart with empty datasets)
    const ctx2 = canvas2.getContext('2d');
    chart2 = new Chart(ctx2, {
        type: 'line',
        data: {
            labels: [],
            datasets: [
                {
                    label: 'S-Pol',
                    data: [],
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.1)',
                    tension: 0.1,
                    fill: false,
                    pointRadius: 0,
                    borderDash: [5, 5]
                },
                {
                    label: 'P-Pol',
                    data: [],
                    borderColor: 'rgb(54, 162, 235)',
                    backgroundColor: 'rgba(54, 162, 235, 0.1)',
                    tension: 0.1,
                    fill: false,
                    pointRadius: 0,
                    borderDash: [10, 5]
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            animation: {
                duration: 300
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Modulation Curves',
                    color: textColor
                },
                legend: {
                    position: 'bottom',
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    max: 1.0,
                    title: {
                        display: true,
                        text: 'Diffraction Efficiency',
                        color: textColor
                    },
                    ticks: {
                        color: textColor
                    },
                    grid: {
                        color: gridColor
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Index Modulation',
                        color: textColor
                    },
                    ticks: {
                        maxTicksLimit: 16,
                        color: textColor
                    },
                    grid: {
                        color: gridColor
                    }
                }
            }
        }
    });
}

export function updateDiffractionEfficiencyChart(wavelengths, spoData, ppData, centerWavelength) {
    if (chart1) {
        // Create wavelength labels formatted to 2 decimal places
        const wavelengthLabels = wavelengths.map(w => w.toFixed(2));
        
        // Calculate average of S-Pol and P-Pol
        const averageData = spoData.map((val, i) => (val + ppData[i]) / 2);
        
        chart1.data.labels = wavelengthLabels;
        chart1.data.datasets[0].data = spoData;
        chart1.data.datasets[1].data = ppData;
        chart1.data.datasets[2].data = averageData;
        
        // Find the index of the closest wavelength to center wavelength
        let closestIndex = 0;
        let minDistance = Math.abs(wavelengths[0] - centerWavelength);
        for (let i = 1; i < wavelengths.length; i++) {
            const distance = Math.abs(wavelengths[i] - centerWavelength);
            if (distance < minDistance) {
                minDistance = distance;
                closestIndex = i;
            }
        }
        
        // Calculate average efficiency at center wavelength
        const avgEfficiencyAtCenter = ((spoData[closestIndex] + ppData[closestIndex]) / 2) * 100;
        
        // Update chart title with average efficiency
        chart1.options.plugins.title.text = `Efficiency Curves - Avg: ${avgEfficiencyAtCenter.toFixed(1)}% at ${centerWavelength}nm`;
        
        // Remove existing plugin
        if (chart1.options.plugins.customLine) {
            delete chart1.options.plugins.customLine;
        }
        
        // Add custom plugin to draw vertical line for center wavelength
        chart1.options.plugins.customLine = {
            closestIndex: closestIndex,
            id: 'customLine'
        };
        
        // Register or update the custom plugin
        const customLinePlugin = {
            id: 'customLine',
            afterDatasetsDraw(chart) {
                const closestIndex = chart.options.plugins.customLine?.closestIndex;
                if (closestIndex === undefined) return;
                
                const xScale = chart.scales.x;
                const yScale = chart.scales.y;
                const ctx = chart.ctx;
                
                // Get pixel position for the closest index
                const xPixel = xScale.getPixelForValue(closestIndex);
                
                if (xPixel !== undefined && !isNaN(xPixel)) {
                    ctx.save();
                    ctx.strokeStyle = 'rgb(255, 0, 0)';
                    ctx.lineWidth = 2;
                    ctx.beginPath();
                    ctx.moveTo(xPixel, yScale.top);
                    ctx.lineTo(xPixel, yScale.bottom);
                    ctx.stroke();
                    ctx.restore();
                }
            }
        };
        
        // Unregister if it exists, then register fresh
        Chart.unregister(customLinePlugin);
        Chart.register(customLinePlugin);
        
        chart1.update();
    } else {
        console.warn("Chart1 not initialized yet");
    }
}

export function updateModulationCurvesChart(wavelengths, spoData, ppData, currentDeltaN, optimalDeltaN) {
    if (chart2) {
        // Create wavelength labels formatted to 2 decimal places
        const wavelengthLabels = wavelengths.map(w => w.toFixed(2));
        
        chart2.data.labels = wavelengthLabels;
        
        // Update or create datasets if needed
        if (chart2.data.datasets.length < 2) {
            chart2.data.datasets.push({
                label: 'P-Pol',
                data: ppData,
                borderColor: 'rgb(54, 162, 235)',
                backgroundColor: 'rgba(54, 162, 235, 0.1)',
                tension: 0.1,
                fill: false,
                pointRadius: 0,
                borderDash: [10, 5]
            });
        } else {
            chart2.data.datasets[0].data = spoData;
            chart2.data.datasets[1].data = ppData;
        }
        
        // Update chart title with optimal delta
        if (optimalDeltaN !== undefined && optimalDeltaN >= 0) {
            chart2.options.plugins.title.text = `Modulation Curves - Optimal ΔN: ${optimalDeltaN.toFixed(3)}`;
        } else {
            chart2.options.plugins.title.text = `Modulation Curves - No Optimal ΔN Found`;
        }
        
        // Remove existing plugin
        if (chart2.options.plugins.customLine) {
            delete chart2.options.plugins.customLine;
        }
        
        // Add custom plugin to draw vertical line for current Delta N if provided
        if (currentDeltaN !== undefined && currentDeltaN >= 0) {
            // Find the index of the closest delta value
            let closestIndex = 0;
            let minDistance = Math.abs(wavelengths[0] - currentDeltaN);
            for (let i = 1; i < wavelengths.length; i++) {
                const distance = Math.abs(wavelengths[i] - currentDeltaN);
                if (distance < minDistance) {
                    minDistance = distance;
                    closestIndex = i;
                }
            }
            
            chart2.options.plugins.customLine = {
                closestIndex: closestIndex,
                id: 'customLine'
            };
            
            // Register or update the custom plugin
            const customLinePlugin = {
                id: 'customLine',
                afterDatasetsDraw(chart) {
                    const closestIndex = chart.options.plugins.customLine?.closestIndex;
                    if (closestIndex === undefined) return;
                    
                    const xScale = chart.scales.x;
                    const yScale = chart.scales.y;
                    const ctx = chart.ctx;
                    
                    // Get pixel position for the closest index
                    const xPixel = xScale.getPixelForValue(closestIndex);
                    
                    if (xPixel !== undefined && !isNaN(xPixel)) {
                        ctx.save();
                        ctx.strokeStyle = 'rgb(255, 0, 0)';
                        ctx.lineWidth = 2;
                        ctx.beginPath();
                        ctx.moveTo(xPixel, yScale.top);
                        ctx.lineTo(xPixel, yScale.bottom);
                        ctx.stroke();
                        ctx.restore();
                    }
                }
            };
            
            // Unregister if it exists, then register fresh
            Chart.unregister(customLinePlugin);
            Chart.register(customLinePlugin);
        }
        
        chart2.update();
    }
}
