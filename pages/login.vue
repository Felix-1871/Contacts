<template>
    <div class="login-page">

        <Head>
            <Title>Login</Title>
        </Head>
        <h1>Login</h1>
        <form @submit.prevent="loginContact">
            <div class="form-group">
                <label for="email">Email address</label>
                <input type="email" v-model="login.email" class="form-control" id="email" aria-describedby="emailHelp"
                    placeholder="Enter email">
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" v-model="login.password" class="form-control" id="password" placeholder="Password">
            </div>
            <button type="submit" class="btn btn-primary">Submit</button>
        </form>
    </div>
</template>

<script>

// simple login script

export default {
    data() {
        return {
            login: {
                email: "",
                password: ""

            },
            passFail: false,
            mailFail: false
        }

    },
    methods: {

        async loginContact() {
            try {
                const response = await $fetch('https://localhost:7160/api/Contact/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.login)
                })
                if (response.message == "Ok") {
                    // create a cookie
                    document.cookie = "loggedIn=true;max-age=60*1000"

                    this.$router.push('/')

                }

            } catch (error) {
                alert("Wrong email or password")
            }
        }


    }
}



</script>

<style scoped>
.login-page {
    margin-top: 40px;
}

.container {
    margin-top: 40px;

}

.display-4 {
    margin-bottom: 40px;
}

.form-group {
    margin-bottom: 20px;
}

.form-group label {
    margin-bottom: 10px;
    margin-right: 10px;
}

.form-group input {
    margin-bottom: 10px;
}
</style>