<script setup lang="ts">
import { numberSearchOperations } from '../consts/searchOperations'
import { useId } from 'vue'
import type { INumberParam } from '@/types/interfaces'

interface INumberFilterProps {
  label: string
}

const uid: string = useId()
const param = defineModel<INumberParam>({ required: true })
defineProps<INumberFilterProps>()
</script>

<template>
  <div class="filter-field">
    <FloatLabel variant="on">
      <InputNumber size="large" fluid :id="uid" v-model="param.Value" :min="0" />
      <label :for="uid">{{ label }}</label>
    </FloatLabel>
    <Select
      v-model="param.Operation"
      :options="numberSearchOperations"
      option-value="Value"
      option-label="Name"
    />
  </div>
</template>
