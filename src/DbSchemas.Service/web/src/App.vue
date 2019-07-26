<template>
  <div id="app">
     <router-view/>
  </div>
</template>

<script>
export default {
  created(){
    // todo 认证权限
    this.$router.push("/")
  },
}
</script>
