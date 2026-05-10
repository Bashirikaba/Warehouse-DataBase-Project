<script setup lang="ts">
import { onMounted } from 'vue'
import { usePositionsActions } from '../viewModels/PositionsViewModel'
import StringFilterField from '@/components/StringFilterField.vue'
import { positionsConfig } from '@/consts/tableConfigs'
import useFilterHelper from '../hooks/useFilterHelper'

const { positions, filter, getAllPositions } = usePositionsActions()
const { getStringParam } = useFilterHelper(filter)

onMounted(() => {
  getAllPositions()
})
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Должности</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <div class="filters" v-for="field in positionsConfig" :key="field.Field">
        <StringFilterField :model-value="getStringParam(field.Field)" :label="field.Label" />
      </div>
      <Button label="Поиск" style="margin-top: 1rem"></Button>
      <Divider></Divider>
      <DataTable :value="positions" striped-rows size="small">
        <Column field="name" header="Должность"></Column>
      </DataTable>
    </template>
  </Card>
</template>
