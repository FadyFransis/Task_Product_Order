
new Vue({
    el: '#vueApp',
    data: {
        order: { orderItems: [] },
        quantity: 1,
        productId:1,
       
        orderStatus: [],
        products:[],
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
        loadAllProducts: function () {
            loaderHandler('show')
           
            axios.get(UploadUrl + 'api/Product/GetAll')
                .then((response) => {
                    loaderHandler('hide')
                    this.products = response?.data.result;



                })
                .catch((error) => {
                    loaderHandler('hide')
                    console.log(error);
                });
        },
        addProduct: function () {
            var addedBefore = this.order.orderItems.find(x => x.productId == this.productId)
            if (addedBefore == undefined) {
                var found = JSON.parse(JSON.stringify(this.products.find(x => x.id == this.productId)));
                found.quantity = JSON.parse(JSON.stringify(this.quantity));
                found.productId = JSON.parse(JSON.stringify(found.id));
                found.id = 0;
                this.order.orderItems.push(found);
            }
            else
                alert("add another order or delete existing one ")
            
        },
        deleteProduct(index) {
            this.order.orderItems.splice(index, 1);
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
            axios.post(UploadUrl + 'API/Order/AddOrder', this.order)
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
       // this.order = order;
      //  this.order.oldStatus = order.orderStatus;
       // this.order.orderDate = order.orderDate.split('T')[0];
        this.loadAllProducts();
        this.bindOrderStatus();

    },
    computed: {
      
        orderTotal: function () {
            var total = 0;
            for (var i = 0; i < this.order.orderItems?.length; i++) {
                 total = total + (this.order.orderItems[i].quantity * this.order.orderItems[i].price); 
            }
            return total;
        }
    }
});