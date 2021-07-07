
new Vue({
    el: '#vueApp',
    data: {
        order: {},
        orderStatus: [],
        MinDate: new Date().toISOString().split('T')[0],
    },

    methods: {
       
        bindOrderStatus: function () {
            this.orderStatus.push({ id: 0, name: 'Submitted from client', nameAr: 'تم اضافته من العميل' })
            this.orderStatus.push({ id: 1, name: 'Confirmed', nameAr: 'تم التاكيد' })
            this.orderStatus.push({ id: 2, name: 'Ontrack', nameAr: 'فى الطريق الى العميل' })
            this.orderStatus.push({ id: 3, name: 'Completed', nameAr: 'تم التوصيل' })
            this.orderStatus.push({ id: 4, name: 'Canceled', nameAr: 'الغاء' })
        },
        isAuthenticated: function () {
            axios.get('/Account/isAuthenticated')
                .then((response) => {
                    if (response.data.result.success === true) {
                        this.saveOrder();
                    }
                    else {
                        var str = "/Account/Login?returnUrl="+window.location.pathname;
                        window.location.href = str;
                    }
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        saveOrder: function () {
            this.order.totalPaid = this.orderTotal;
            loaderHandler('show')
            axios.post(UploadUrl + 'API/Order/UpdateOrder', this.order)
                .then((response) => {
                    loaderHandler('hide')
                    if (response.data.isError === false) {
                        fireSuccessAlert('تم حفظ الطلب بنجاح', 'order saved successfully');
                        window.location.href = "/Order"
                    }

                    else
                        fireErrorAlert('خطأ فى خفظ الطلب', 'Error in saving order');
                        
                })
                .catch((error) => {
                    loaderHandler('hide')
                    fireErrorAlert('خطأ فى خفظ الطلب', 'Error in saving order');
                   
                });
        }
    },
    mounted() {
       this.order = order;
        this.order.oldStatus = order.orderStatus;
        this.order.orderDate = order.orderDate.split('T')[0];
        this.bindOrderStatus();

    },
    computed: {
      
        orderTotal: function () {
            var total = this.order.deliveryFees - this.order.discountAmount;
            for (var i = 0; i < this.order.orderItems?.length; i++) {
                total = total + (this.order.orderItems[i].quantity * this.order.orderItems[i].price); 
            }
            return total;
        }
    }
});