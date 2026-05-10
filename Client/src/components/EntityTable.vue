<script setup lang="ts">
import type { IEntity, ITableConfigItem } from '@/types/interfaces'
interface IEntityTableProps {
  config: ITableConfigItem[]
}
const entityList = defineModel<IEntity[]>({ required: true })
defineProps<IEntityTableProps>()
</script>
<template>
  <DataTable :value="entityList" striped-rows size="small" show-gridlines>
    <Column
      v-for="row in config"
      :key="row.Field"
      :field="row.Field"
      :header="row.Label"
      :data-type="row.Type"
      :hidden="row.Hidden"
    ></Column>
  </DataTable>
</template>
