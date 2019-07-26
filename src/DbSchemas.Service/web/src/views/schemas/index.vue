<template>
    <el-container>
      <el-header>
        Emr DbSchemas
      </el-header>
      <el-container style="margin:10px;">
          <el-aside style="width:300px">
            <el-input placeholder="表名" v-model="schemaTable" @keyup.enter.native="getTables" clearable @clear="getTables"></el-input>
            <div style="text-align:left;height:800px;">
              <el-scrollbar style="height:100%;">
                  <el-menu class="el-menu-demo" mode="vertical" @select="handleSelect">
                    <el-menu-item :index="menu.tablename" v-for="menu in tableMeaus" v-bind:key="menu.tablename" >
                      <span slot="title">
                        <div v-if="menu.description != null">
                          <el-tooltip placement="top">
                            <div slot="content">{{menu.description}}</div>
                            <span>{{menu.tablename}}</span>
                          </el-tooltip>
                        </div>
                       <div v-else>
                          <span>{{menu.tablename}}</span>
                       </div>
                      </span>
                    </el-menu-item>
                  </el-menu>
              </el-scrollbar>
            </div>
          </el-aside>
          <el-main>
            <div style="height:800px;">
              <el-scrollbar style="height:100%;">
              <el-table :data="columnsData" >
                  <el-table-column prop="columnname" label="列名"></el-table-column>
                  <el-table-column prop="type" label="类型"></el-table-column>
                  <el-table-column prop="notnull" label="不为空" :formatter="fmtnotnull"></el-table-column>
                  <el-table-column prop="description" label="备注"></el-table-column>
              </el-table>
              </el-scrollbar>
            </div>
          </el-main>
      </el-container>
    </el-container>
</template>

<script>
export default {
  created(){
    this.apiServer = localStorage.getItem("apiServer");
    this.getTables();
  },
  data(){
     return {
        apiServer:"",
        schemaTable:"",
        tableMeaus: [],
        columnsData:[]
      };
  },
  methods: {
    getTables(){
      let url = "api/DbSchemas/GetSchemaTables?schemaTable=" + this.schemaTable; 
      this.axios.get(url).then(response =>{
        console.log(response.data);
        let data = response.data.data;
        this.tableMeaus = data;
        if(this.schemaTable != "" && response.data.code == 0){
          this.handleSelect(this.schemaTable);
        }
      })
    },
    handleSelect(key){
      console.log(key );
      let url = "api/DbSchemas/GetTableInfos?tableName=" + key; 
      this.axios.get(url).then(response =>{
        console.log(response.data);
        let data = response.data.data;
        this.columnsData = data; 
      })
    },
    fmtnotnull(row,col,cellvalue,index){
      return cellvalue+'';
    }
  }
}
</script>

<style>
body{
  margin: 0px;
  border:0px;
}
#app {
  font-family: Helvetica, sans-serif;
  text-align: center;
}
.el-scrollbar__wrap {
  overflow-x: hidden;
}
.el-header{
    height: 60px;
    line-height: 60px;
    background: rgb(55, 60, 75);
    color: #fff;
}
</style>
